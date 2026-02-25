import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { BookService } from '../../services/book.service';
import { BookModel } from '../../models/book.model';
import { Header } from '../header/header';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-books',
  imports: [Header, FormsModule],
  templateUrl: './books.html',
  styleUrl: './books.scss',
})
export class Books implements OnInit{
  private bookService = inject(BookService)
  private router = inject(Router);

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

  goToCreate() {
    this.router.navigate(['/books/new']);
  }

  goToEdit(id: number) {
    this.router.navigate(['/books/edit', id]);
  }

  deleteBook(book: BookModel) {
    if (book.isActive) {
      alert('Não é permitido remover um livro que esteja ativo');
      return;
    }

    if (confirm(`Tem certeza que deseja excluir o livro "${book.title}"?`)) {
      this.bookService.deleteBook(book.id).subscribe({
        next: () => {
          this.books.update(currentBooks => currentBooks.filter(b => b.id !== book.id));
        },
        error: (err) => alert(err.error?.message || 'Erro ao excluir o livro')
      });
    }
  }
}
