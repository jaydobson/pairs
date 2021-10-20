import { Component, OnInit } from '@angular/core';
import { Game } from '../../../models';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../../services';
import { GameStatus, UpdateGameRequest, UpdateGameResponse } from '../../../Models';

@Component({
  selector: 'board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent implements OnInit {
  api: ApiService;
  game: Game;
  gameStatusEnum = GameStatus;
  flipped: string[] = [];
  isSaving = false;
  isCompleted = false;

  constructor(private apiService: ApiService, private route: ActivatedRoute) {
    this.api = apiService;
  }

  async ngOnInit() {
    const id = this.route.snapshot.params.id;
    await this.loadGame(id);
    this.preloadImages();
  }

  async loadGame(id: string) {
    this.game = await this.api.loadGame(id);
    // TODO: Redirect to home on err w/ toast
  }

  async onClick(id: string) {
    if (this.isSaving || this.isCompleted) {
      return;
    }

    this.isSaving = true;
    const card1 = this.game.cards.filter(c => c.id === this.flipped[0])[0];

    // User is unflipping first card
    if (card1 && card1.id === id) {
      this.flipped = [];
      await this.delay(500);
      card1.flipped = false;
      return;
    }

    if (this.flipped.length < 2) {
      const card = this.game.cards.filter(c => c.id === id)[0];
      card.flipped = true;
      this.flipped.push(card.id);
    }

    // User has flipped two cards
    if (this.flipped.length == 2) {
      const card2 = this.game.cards.filter(c => c.id === this.flipped[1])[0];

      const updateRequest: UpdateGameRequest = {
        id: this.game.id,
        cardIds: [card1.id, card2.id]
      };

      try {
        const result = await this.api.updateGame(updateRequest);
        this.game.turns = result.game.turns;

        if (result.game.status == this.gameStatusEnum.completed) {
          this.isCompleted = true;
        }
      } catch {
        // TODO: Log error
      } finally {
        this.isSaving = false;
      }

      if (card1.cardId !== card2.cardId) {
        await this.delay(500);
        card2.flipped = false;
        card1.flipped = false;
      }

      this.flipped = [];
    }

    this.isSaving = false;
  }

  private delay = time => new Promise(res => setTimeout(() => res(), time));

  private preloadImages() {    
    this.game.cards.forEach(c => {
      var img = new Image();
      img.src = c.imagePath;
    });    
  }
}
