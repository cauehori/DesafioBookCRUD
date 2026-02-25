using System;
using BookCRUD.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookCRUD.Infrastructure;

public class BookDbContext(DbContextOptions<BookDbContext> options) : DbContext(options)
    {
    public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookDbContext).Assembly);
        }
    }
