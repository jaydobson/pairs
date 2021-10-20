export interface Game {
  id: string;
  cards: GameCard[];
  status: GameStatus;
  turns: number;
}

export interface Card {
  id: string;
  name: string;
  imagePath;
}

export interface GameCard {
  id: string;
  matched: boolean;
  cardId: string;
  card: Card;
  flipped: boolean;
  imagePath: string;
}

export interface UpdateGameRequest {
  id: string;
  cardIds: string[];
}

export interface UpdateGameResponse {  
  completed: boolean,
  isMatch: boolean;
  game: Game;  
}

export interface HistoryItem {  
  finish: string;
  numCards: number;
  start: string;
  turns: number;
}

export enum GameStatus {
  abandoned = 0,
  completed,
  inProgress
}
