namespace BookCRUD.Application.DTOs;

public record class BookUpdateDTO
(
    int Id,
    string Title,
    string Author,
    string Category,
    int TotalPages,
    bool IsActive
);

