import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChatMessage } from '../models/chat-message.model';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private baseUrl = '';

  constructor(private http: HttpClient) {}

  // Получение всех сообщений чата
  getAllMessages(): Observable<ChatMessage[]> {
    return this.http.get<ChatMessage[]>(this.baseUrl);
  }

  // Получение сообщения по ID
  getMessageById(id: string): Observable<ChatMessage> {
    return this.http.get<ChatMessage>(`${this.baseUrl}/${id}`);
  }

  // Отправка нового сообщения
  sendMessage(message: ChatMessage): Observable<void> {
    return this.http.post<void>(this.baseUrl, message);
  }

  // Обновление сообщения
  updateMessage(id: string, message: ChatMessage): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, message);
  }

  // Удаление сообщения
  deleteMessage(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
