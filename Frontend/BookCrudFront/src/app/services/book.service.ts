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

  getBookById(id: number): Observable<BookModel> {
    return this.http.get<BookModel>(`${this.apiUrl}/${id}`);
  }

  createBook(book: Partial<BookModel>): Observable<any> {
    return this.http.post(this.apiUrl, book);
  }

  updateBook(id: number, book: Partial<BookModel>): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, book);
  }

  deleteBook(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
