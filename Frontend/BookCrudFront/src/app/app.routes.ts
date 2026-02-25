import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { authGuardGuard } from './guards/auth.guard-guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  
  { path: 'login', component: LoginComponent },

  { 
    path: 'books', 
    loadComponent: () => import('./components/books/books').then(m => m.Books),
    canActivate: [authGuardGuard] 
  },

  {
    path: 'books/new',
    loadComponent: () => import('./components/form/form').then(m => m.Form),
    canActivate: [authGuardGuard]
  },

  {
    path: 'books/edit/:id',
    loadComponent: () => import('./components/form/form').then(m => m.Form),
    canActivate: [authGuardGuard]
  },
  
  { path: '**', redirectTo: 'login' }
];
