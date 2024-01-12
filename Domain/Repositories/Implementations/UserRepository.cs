using System.Linq.Expressions;
using Domain.DTOs;
using Domain.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class UserRepository : ARepository<User>, IUserRepository
{
    public UserRepository(YDbContext context) : base(context) { }
    
    public async Task<bool> IsUsernameAvailable(string username)
    {
        return await Table.AllAsync(u => u.Username != username);
    }
    
    public IQueryable<User> Read(int id) {
        return Table.Where(e => e.Id == id);
    }

    public IQueryable<DefaultUserDto> ReadAll()
    {
        return Table.Select(u => u.Adapt<DefaultUserDto>());
    }
}