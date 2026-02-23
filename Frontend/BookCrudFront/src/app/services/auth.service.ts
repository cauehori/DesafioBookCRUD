import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private apiUrl = 'http://localhost:5221/api/auth';

  login(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials).pipe(
      tap((res: any) => {
        localStorage.setItem('jwt_token', res.token);
      })
    );
  }
  
  logout() {
    localStorage.removeItem('jwt_token');
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('jwt_token');
  }
}
