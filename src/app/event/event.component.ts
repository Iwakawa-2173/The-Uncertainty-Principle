import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EventService } from '../services/event.service';
import { ResponseService } from '../services/response.service';
import { ChatService } from '../services/chat.service';
import { Event } from '../models/event.model';
import { ScoreScale } from '../models/score-scale.model';
import { ChatMessage } from '../models/chat-message.model';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css']
})
export class EventComponent implements OnInit {
  currentEvent!: Event;
  scoreScales!: ScoreScale[];
  playerId: number = 1; // Замените на реальный ID игрока
  chatMessages: ChatMessage[] = [];
  newMessage: string = '';

  constructor(
    private eventService: EventService,
    private responseService: ResponseService,
    private chatService: ChatService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadEvent();
    this.loadScoreScales();
    this.loadChatMessages(); // Загружаем сообщения чата
  }

  loadEvent() {
    const eventId = 'some-event-id'; // Замените на реальный ID события
    this.eventService.getEventById(eventId).subscribe(event => {
      this.currentEvent = event;
    });
  }

  loadScoreScales() {
    this.eventService.getScoreScales().subscribe(scales => {
      this.scoreScales = scales;
      this.checkEnding();
    });
  }

  loadChatMessages() {
    this.chatService.getAllMessages().subscribe(messages => {
      this.chatMessages = messages;
    });
  }

  selectOption(optionIndex: number) {
    const response = {
      id: '',
      playerId: this.playerId,
      eventId: this.currentEvent.id,
      responseOption: optionIndex + 1 
    };

    this.responseService.sendResponse(response).subscribe(() => {
      this.loadEvent(); 
      this.loadScoreScales(); 
    });
  }

  checkEnding() {
    if (this.scoreScales) {
      if (this.scoreScales[0].scale1 > someThreshold) {
        this.router.navigate(['/ending-a']);
      } else if (this.scoreScales[1].scale2 > someThreshold) {
        this.router.navigate(['/ending-b']);
      }
    }
  }

  sendMessage() {
    if (this.newMessage.trim()) {
      const message: ChatMessage = { id: '', message: this.newMessage }; // ID будет создан на сервере
      this.chatService.sendMessage(message).subscribe(() => {
        this.loadChatMessages(); // Обновляем список сообщений после отправки
        this.newMessage = ''; // Очищаем поле ввода
      });
    }
  }
}
