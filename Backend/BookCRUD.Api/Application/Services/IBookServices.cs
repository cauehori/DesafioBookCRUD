using System;
using BookCRUD.Api.Domain;
using BookCRUD.Api.DTOs;

namespace BookCRUD.Api.Application.Services;

public interface IBookServices
{
    Task<IEnumerable<Book>> GetAllBooks();
    Task AddBook(BookCreateDTO bookDto);
    Task UpdateBook(BookUpdateDTO bookDto);
    Task DeleteBook(int id);
}
