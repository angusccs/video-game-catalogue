import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Game {
  id: number;
  title: string;
  platform?: string | null;
}

@Injectable({ providedIn: 'root' })
export class GamesApiService {
  // Adjust port to match backend
  private readonly baseUrl = 'http://localhost:5240/api/videogames';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Game[]> {
    return this.http.get<Game[]>(this.baseUrl);
  }

  getById(id: number): Observable<Game> {
    return this.http.get<Game>(`${this.baseUrl}/${id}`);
  }

  update(game: Game): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${game.id}`, game);
  }

  delete(id: number) {
  return this.http.delete<void>(`${this.baseUrl}/${id}`);
}

}
