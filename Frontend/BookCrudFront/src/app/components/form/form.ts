import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Header } from '../header/header';
import { BookService } from '../../services/book.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-form',
  imports: [ReactiveFormsModule, Header],
  templateUrl: './form.html',
  styleUrl: './form.scss',
})
export class Form implements OnInit {
  private fb = inject(FormBuilder);
  private bookService = inject(BookService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  isEditMode = signal(false);
  isSaving = signal(false);
  bookId: number | null = null;

  bookForm = this.fb.group({
    title: ['', [Validators.required, Validators.maxLength(50)]],
    author: ['', [Validators.required, Validators.maxLength(50)]],
    category: [''],
    totalPages: [1, [Validators.required, Validators.min(1)]],
    isActive: [true]
  });

  ngOnInit() {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isEditMode.set(true);
      this.bookId = +idParam;
      this.loadBook(this.bookId);
    }
  }

  loadBook(id: number) {
    this.bookService.getBookById(id).subscribe({
      next: (book) => {
        this.bookForm.patchValue(book);
    },
    error: (err) => {
        console.error('Erro ao buscar os dados do livro:', err);
        alert('Não foi possível carregar os dados deste livro para edição');
      }
    });
  }

  onSubmit() {
    if (this.bookForm.valid) {
      this.isSaving.set(true);

      const formData = {
        title: this.bookForm.value.title ?? '',
        author: this.bookForm.value.author ?? '',
        category: this.bookForm.value.category ?? '',
        totalPages: this.bookForm.value.totalPages ?? 1,
        isActive: this.bookForm.value.isActive ?? true
      };

      if (this.isEditMode() && this.bookId) {
        const bookToUpdate = { id: this.bookId, ...formData };

        this.bookService.updateBook(this.bookId, bookToUpdate).subscribe({
          next: () => this.router.navigate(['/books']),
          error: () => { alert('Erro ao atualizar'); this.isSaving.set(false); }
        });
      } else {
        this.bookService.createBook(formData).subscribe({
          next: () => this.router.navigate(['/books']),
          error: () => { alert('Erro ao cadastar o livro'); this.isSaving.set(false); }
        });
      }
    }
  }

  cancel() {
    this.router.navigate(['/books']);
  }
}
