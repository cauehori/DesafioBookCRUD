using System;
using BookCRUD.Application.DTOs;
using BookCRUD.Application.Repositories;
using BookCRUD.Domain;

namespace BookCRUD.Application.Services;

public class BookServices(IBookRepository repository) : IBookServices
{
    private readonly IBookRepository _repository = repository;

    public async Task AddBook(BookCreateDTO dto)
    {
        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            Category = dto.Category,
            TotalPages = dto.TotalPages,
            IsActive = dto.IsActive
        };
        
        await _repository.Add(book);
    }

    public async Task DeleteBook(int id)
    {
        var book = await _repository.GetById(id);
        if (book == null)
        {
            throw new Exception("Livro não encrontrado.");
        }

        if (book.IsActive)
        {
            throw new InvalidOperationException("Não é permitido remover um livro que esteja ativo");
        }

        await _repository.Delete(book);
    }

    public async Task<IEnumerable<Book>> GetAllBooks() =>  await _repository.GetAll();

    public async Task UpdateBook(BookUpdateDTO dto)
    {
        var book = await _repository.GetById(dto.Id);
        if (book == null)
        {
            throw new Exception("Livro não encontrado.");
        }

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.Category = dto.Category;
        book.TotalPages = dto.TotalPages;
        book.IsActive = dto.IsActive;

        await _repository.Update(book);
    }
}
