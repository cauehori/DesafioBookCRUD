import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookModel } from '../models/book.model';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private http = inject(HttpClient);

  private apiUrl = 'http://localhost:5221/api/book';

  getBooks(): Observable<BookModel[]> {
    return this.http.get<BookModel[]>(this.apiUrl);
  }
}
