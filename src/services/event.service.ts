import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Event } from '../models/event.model'; 
import { ScoreScale } from '../models/score-scale.model';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private baseUrl = ''; 

  constructor(private http: HttpClient) {}

  // Получение всех событий
  getAllEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(this.baseUrl);
  }

  // Получение события по ID
  getEventById(id: string): Observable<Event> {
    return this.http.get<Event>(`${this.baseUrl}/${id}`);
  }

  // Получение текущих шкал
  getScoreScales(): Observable<ScoreScale[]> {
    return this.http.get<ScoreScale[]>('https://your-api-url.com/score-scales'); // URL для получения шкал
  }

  // Отправка ответа на событие
  submitAnswer(eventId: string, optionIndex: number): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/${eventId}/answer`, { optionIndex });
  }
}
