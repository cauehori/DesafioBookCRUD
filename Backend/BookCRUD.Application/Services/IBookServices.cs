using System;
using BookCRUD.Application.DTOs;
using BookCRUD.Domain;

namespace BookCRUD.Application.Services;

public interface IBookServices
{
    Task<IEnumerable<Book>> GetAllBooks();
    Task AddBook(BookCreateDTO bookDto);
    Task UpdateBook(BookUpdateDTO bookDto);
    Task DeleteBook(int id);
    Task<Book?> GetBookById(int id);
}
