import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { email } from '@angular/forms/signals';

@Component({
  selector: 'app-login.component',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  isLoading = signal(false);

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  onSubmit() {
    if (this.loginForm.valid) {
      this.isLoading.set(true);

      this.authService.login(this.loginForm.value).subscribe({
        next: () => {
          this.isLoading.set(false);
          this.router.navigate(['/books']);
        },
        error: (err) => {
          this.isLoading.set(false);
          alert('Erro no login, verifique as credenciais');
        }
      });
    }
  }
}
