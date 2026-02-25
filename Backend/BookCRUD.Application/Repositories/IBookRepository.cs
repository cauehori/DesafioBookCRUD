using System;
using BookCRUD.Domain;

namespace BookCRUD.Application.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAll();
    Task<Book?> GetById(int id);
    Task Add(Book book);
    Task Update(Book book);
    Task Delete(Book book);
}
