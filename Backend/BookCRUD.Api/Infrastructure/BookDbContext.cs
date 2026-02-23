using System;
using BookCRUD.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookCRUD.Api.Infrastructure;

public class BookDbContext(DbContextOptions<BookDbContext> options) : DbContext(options)
    {
    public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookDbContext).Assembly);
        }
    }
