import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as models from '../models';

@Injectable()
export class ApiService {  
  private baseUrl: string;
  private http: HttpClient;

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  loadGame(id: string): Promise<models.Game> {
    return this.http.get<models.Game>(this.baseUrl + `game/play/${id}`).toPromise();
  }

  startGame(numCards: number): Promise<models.Game> {
    return this.http.post<models.Game>(this.baseUrl + 'game/start', { numCards: numCards }).toPromise();
  }

  updateGame(updateGameRequest: models.UpdateGameRequest): Promise<models.UpdateGameResponse> {
    return this.http.put<models.UpdateGameResponse>(this.baseUrl + 'game/update', updateGameRequest).toPromise();
  }

  getActive(): Promise<models.Game> {
    return this.http.get<models.Game>(this.baseUrl + 'game/active').toPromise();
  }

  getHistory(): Promise<models.HistoryItem[]> {
    return this.http.get<models.HistoryItem[]>(this.baseUrl + 'game/history').toPromise();
  }
}
