using System;
using BookCRUD.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookCRUD.Api.Infrastructure.Repositories;

public class BookRepository(BookDbContext context) : IBookRepository
{
    private readonly BookDbContext _context = context;

    public async Task<IEnumerable<Book>> GetAll() => await _context.Books.ToListAsync();

    public async Task<Book?> GetById(int id) => await _context.Books.FindAsync(id);

    public async Task Add(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}
