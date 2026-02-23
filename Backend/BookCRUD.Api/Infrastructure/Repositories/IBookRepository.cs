using System;
using BookCRUD.Api.Domain;

namespace BookCRUD.Api.Infrastructure.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAll();
    Task<Book?> GetById(int id);
    Task Add(Book book);
    Task Update(Book book);
    Task Delete(Book book);
}
