import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../services';
import { Game, HistoryItem } from '../models';
import { AuthorizeService } from '../../api-authorization/authorize.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  api: ApiService;
  authSerivce: AuthorizeService;
  form: FormGroup;
  history: HistoryItem[] = [];
  isAuthenticated = false;
  auth$ = null;
  activeGame: Game;

  constructor(private apiService: ApiService,
    private authorizeSerivce: AuthorizeService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router,) {
      this.api = apiService;
      this.authSerivce = authorizeSerivce;
  }

  async ngOnInit() {
    this.auth$ = this.authSerivce.isAuthenticated();

    this.auth$.subscribe(o => {
      this.isAuthenticated = o;
    });

    this.form = this.buildForm();

    await this.loadHistory();
    await this.getActive();
  }

  get numCards(): FormControl {
    return this.form.get('numCards') as FormControl;
  }

  async startGame() {
    if (this.form.invalid) {
      return;
    }

    try {
      const newGame = await this.api.startGame(this.numCards.value);

      if (newGame) {
        this.router.navigate(['../game', newGame.id], { relativeTo: this.route });
      }
    }
    catch (err) {
      if (err.error.errors.NumCards) {
        // TODO: This would be better suited as a toast
        alert(err.error.errors.NumCards[0]);
      }
    }
  }

  private buildForm(): FormGroup {
    this.form = this.fb.group({
      numCards: [16, [Validators.required]]
    });

    return this.form;
  }

  private async loadHistory() {
    this.history = await this.api.getHistory();
  }

  private async getActive() {
    this.activeGame = await this.api.getActive();
  }
}
