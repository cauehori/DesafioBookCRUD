using BookCRUD.Application.Repositories;
using BookCRUD.Application.Services;
using BookCRUD.Domain;
using Moq;
using Xunit;

namespace BookCRUD.Test;

public class BookServiceTest
{
    [Fact]
    public async Task DeleteBookFalse()
    {
        var mockRepository = new Mock<IBookRepository>();

        var activeBook = new Book
        {
            Id = 1,
            Title = "Livro Teste",
            IsActive = true
        };

        mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(activeBook);  

        var service = new BookServices(mockRepository.Object);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteBook(1));

        Assert.Equal("Não é permitido remover um livro que esteja ativo", exception.Message);

        mockRepository.Verify(repo => repo.Delete(It.IsAny<Book>()), Times.Never);
    }
}
