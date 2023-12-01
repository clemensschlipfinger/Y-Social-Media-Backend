using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class UserRepository : ARepository<User>, IUserRepository
{
    public UserRepository(YDbContext context) : base(context) { }
}