import { Component, OnInit } from '@angular/core';
import { ChatService } from '../services/chat.service';
import { ChatMessage } from '../models/chat-message.model';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  messages: ChatMessage[] = [];
  newMessage: string = '';

  constructor(private chatService: ChatService) {}

  ngOnInit() {
    this.loadMessages();
  }

  loadMessages() {
    this.chatService.getAllMessages().subscribe(messages => {
      this.messages = messages;
    });
  }

  sendMessage() {
    if (this.newMessage.trim()) {
      const message: ChatMessage = { id: '', message: this.newMessage }; // ID может быть пустым на этапе создания
      this.chatService.sendMessage(message).subscribe(() => {
        this.loadMessages(); // Обновляем список сообщений после отправки
        this.newMessage = ''; // Очищаем поле ввода
      });
    }
  }
}
