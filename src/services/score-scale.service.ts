import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ScoreScale } from '../models/score-scale.model'; 

@Injectable({
  providedIn: 'root'
})
export class ScoreScaleService {
  private baseUrl = ''; 

  constructor(private http: HttpClient) {}

  // Получение всех шкал очков
  getAllScoreScales(): Observable<ScoreScale[]> {
    return this.http.get<ScoreScale[]>(this.baseUrl);
  }

  // Получение шкалы по ID
  getScoreScaleById(id: string): Observable<ScoreScale> {
    return this.http.get<ScoreScale>(`${this.baseUrl}/${id}`);
  }

  // Добавление новой шкалы
  addScoreScale(scale: ScoreScale): Observable<void> {
    return this.http.post<void>(this.baseUrl, scale);
  }

  // Обновление шкалы
  updateScoreScale(id: string, scale: ScoreScale): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, scale);
  }

  // Удаление шкалы
  deleteScoreScale(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
