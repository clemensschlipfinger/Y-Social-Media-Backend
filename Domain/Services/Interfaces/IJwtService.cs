using Model.Entities;

namespace Domain.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(User? user);
}