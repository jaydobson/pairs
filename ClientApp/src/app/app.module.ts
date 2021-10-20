import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BoardComponent } from './game';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ApiService } from './services';
import { ReactiveFormsModule } from '@angular/forms';

const PROVIDERS = [
  ApiService,
  AuthorizeService,
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    BoardComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'game/:id', component: BoardComponent, canActivate: [AuthorizeGuard] },
    ])
  ],
  providers: [
    ...PROVIDERS,
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
