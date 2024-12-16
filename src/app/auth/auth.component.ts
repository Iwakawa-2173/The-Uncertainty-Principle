import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent {
  playerName: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.authService.login(this.playerName).subscribe(() => {
      // После успешной аутентификации перенаправляем на страницу событий
      this.router.navigate(['/event']);
    }, error => {
      // Обработка ошибок (например, показать сообщение об ошибке)
      console.error('Ошибка аутентификации:', error);
    });
  }
}
