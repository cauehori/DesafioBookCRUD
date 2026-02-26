using System;
using BookCRUD.Application.DTOs;

namespace BookCRUD.Application.Services;

public interface IAuthService
{
    string? Authenticate(LoginDTO loginDto);
}
