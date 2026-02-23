namespace BookCRUD.Api.DTOs;

public record class BookCreateDTO
(
    string Title,
    string Author,
    string Category,
    int TotalPages,
    bool IsActive
);
