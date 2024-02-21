using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface IJwtService
{
    string GenerateToken(User? user);
}