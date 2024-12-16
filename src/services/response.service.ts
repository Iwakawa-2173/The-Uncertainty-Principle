import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class ResponseService {
  private baseUrl = '';

  constructor(private http: HttpClient) {}

  // Получение всех ответов
  getAllResponses(): Observable<Response[]> {
    return this.http.get<Response[]>(this.baseUrl);
  }

  // Получение ответа по ID
  getResponseById(id: string): Observable<Response> {
    return this.http.get<Response>(`${this.baseUrl}/${id}`);
  }

  // Отправка нового ответа
  sendResponse(response: Response): Observable<void> {
    return this.http.post<void>(this.baseUrl, response);
  }

  // Обновление ответа
  updateResponse(id: string, response: Response): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, response);
  }

  // Удаление ответа
  deleteResponse(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
