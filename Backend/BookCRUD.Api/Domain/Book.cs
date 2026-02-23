using System;

namespace BookCRUD.Api.Domain
{
    public class Book
    {
        public int Id { get; set; }
        
        // Obrigatório, Máximo de 50 caracteres
        public string Title { get; set; } = string.Empty; 
        
        // Obrigatório, Máximo de 50 caracteres
        public string Author { get; set; } = string.Empty;
        
        // Opcional, Dropdown com lista fixa
        public string? Category { get; set; }
        
        // Numérico, Maior que 0
        public int TotalPages { get; set; }
        
        // Booleano
        public bool IsActive { get; set; } = true; 
    }
}
