using System.Text.RegularExpressions;
using Domain.DTOs;
using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Mapper;
using Domain.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class UserRepository : ARepository<User>, IUserRepository
{
    private readonly FromMapper<User, Graphql.Types.User> mapper;


    public UserRepository(IDbContextFactory<YDbContext> dbContextFactory, FromMapper<User, Graphql.Types.User> mapper) : base(dbContextFactory)
    {
        this.mapper = mapper;
    }

    public async Task<bool> IsUsernameAvailable(string username)
    {
        return await Table.AllAsync(u => u.Username != username);
    }

    public async Task<User?> Read(int id)
    {
        return await this.Table
            .Include(u => u.Following).ThenInclude(f => f.Following)
            .Include(u => u.Follower).ThenInclude(f => f.Follower)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> Read(string username)
    {
        return await this.Table
            .Include(u => u.Following).ThenInclude(f => f.Following)
            .Include(u => u.Follower).ThenInclude(f => f.Follower)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<Graphql.Types.User?> ReadGraphqlUser(int id)
    {
        var user = await Read(id);
        if (user is null) {  return null; }
        return mapper.mapFrom(user);
    }

    public async Task<Graphql.Types.User?> ReadGraphqlUser(string username)
    {
        var user = await Read(username);
        if (user is null) {  return null; }
        return mapper.mapFrom(user);
    }

    public async Task<UserResult> ReadUser(UserInput input)
    {
        var user = await ReadGraphqlUser(input.UserId);
        return new UserResult(user);
    }

    public async Task<UsersResult> ReadUsers(UsersInput input)
    {
        IQueryable<User> usersQuery = Table
            .Include(u => u.Following).ThenInclude(f => f.Following)
            .Include(u => u.Follower).ThenInclude(f => f.Follower);

        if (input.Filter is not null)
            usersQuery = usersQuery.Where(u => u.Username.Contains(input.Filter));

        switch (input.Direction)
        {
            case SortDirection.ASC:
                switch (input.Sorting)
                {
                    case SortUsers.USERNAME:
                        usersQuery = usersQuery.OrderBy(u => u.Username);
                        break;
                    case SortUsers.FIRST_NAME:
                        usersQuery = usersQuery.OrderBy(u => u.FirstName);
                        break;
                    case SortUsers.LAST_NAME:
                        usersQuery = usersQuery.OrderBy(u => u.LastName);
                        break;
                    default:
                        usersQuery = usersQuery.OrderBy(u => u.Id);
                        break;
                }
                break;
            case SortDirection.DSC:
                switch (input.Sorting)
                {
                    case SortUsers.USERNAME:
                        usersQuery = usersQuery.OrderByDescending(u => u.Username);
                        break;
                    case SortUsers.FIRST_NAME:
                        usersQuery = usersQuery.OrderByDescending(u => u.FirstName);
                        break;
                    case SortUsers.LAST_NAME:
                        usersQuery = usersQuery.OrderByDescending(u => u.LastName);
                        break;
                    default:
                        usersQuery = usersQuery.OrderByDescending(u => u.Id);
                        break;
                }
                break;
        }

        usersQuery = usersQuery.Skip(input.Offset).Take(input.Limit);

        var count = await usersQuery.CountAsync();
        var users = await usersQuery.ToListAsync();
        
        return new UsersResult(users.Select(u => mapper.mapFrom(u)).ToList(), count);
    }
}