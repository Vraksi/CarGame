import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Highscore } from '../models/highscore';

@Injectable({
  providedIn: 'root'
})
export class HighscoreService {

  private highscoreUrl = 'api/highscores';
  highscore: Highscore;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json'})
  };
  constructor(private http: HttpClient) { }

  getHighscores(): Observable<Highscore[]> {
    return this.http.get<Highscore[]>(this.highscoreUrl)
  }
  getHighscoresId(id): Observable<Highscore> {
    return this.http.get<Highscore>(`${this.highscoreUrl}/${id}`);
  }
  addHighscores(data): Observable<Highscore> {
    return this.http.post<Highscore>(this.highscoreUrl, data, this.httpOptions);
  }
  editHighscores(data) {
    return this.http.put(`${this.highscoreUrl}/${data.highscoresId}`, data, this.httpOptions)
      .pipe(tap(_ => console.log(`Updated Highscores with id ${data.highscoresId}`)))
  }
  deleteHighscores(data) {
    return this.http.delete<Highscore>(`${this.highscoreUrl}/${data.highscoresId}`, data, /*this.httpOptions*/)
      .pipe(tap(_ => console.log(`deleted Highscores with id = ${data.highscoresId}`)))
  }
}
