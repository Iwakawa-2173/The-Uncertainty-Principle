import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
 providedIn: 'root'
})
export class AuthService {
 constructor(private http: HttpClient) {}

 login(playerName: string): Observable<void> {
     return this.http.post<void>('/api/auth/login', { playerName });
 }
}
