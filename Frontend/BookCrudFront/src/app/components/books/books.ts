import { Component, computed, inject, signal } from '@angular/core';
import { BookService } from '../../services/book.service';
import { BookModel } from '../../models/book.model';
import { Header } from '../header/header';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-books',
  imports: [Header, FormsModule],
  templateUrl: './books.html',
  styleUrl: './books.scss',
})
export class Books {
  private bookService = inject(BookService)

  books = signal<BookModel[]>([]);
  isLoading = signal<boolean>(false);
  searchQuery = signal<string>('');

  filteredBooks = computed(() => {
    const query = this.searchQuery().toLowerCase().trim();
    const allBooks = this.books();

    if (!query) return allBooks;

    return allBooks.filter(book =>
      book.title.toLowerCase().includes(query) ||
      book.author.toLowerCase().includes(query) ||
      (book.category && book.category.toLowerCase().includes(query))
    );
  });

  ngOnInit(){
    this.loadBooks();
  }

  loadBooks() {
    this.isLoading.set(true);
    this.bookService.getBooks().subscribe({
      next: (data) => {
        this.books.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Erro ao buscar livros', err);
        this.isLoading.set(false);
      }
    });
  }
}
