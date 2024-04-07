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
    private readonly FromMapper<User, Graphql.Types.User> _mapper;


    public UserRepository(IDbContextFactory<YDbContext> dbContextFactory, FromMapper<User, Graphql.Types.User> mapper) : base(dbContextFactory)
    {
        this._mapper = mapper;
    }
    private IQueryable<User> PreparedStatement() => Table
        .Include(u => u.Follower).ThenInclude(u => u.Follower)
        .Include(u => u.Following).ThenInclude(u => u.Following)
        .AsNoTracking()
                    .AsQueryable(); 

    public async Task<bool> IsUsernameAvailable(string username)
    {
        return await Table.AllAsync(u => u.Username != username);
    }

    public async Task<User?> Read(int id)
    {
        return await this.PreparedStatement().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> Read(string username)
    {
        return await this.PreparedStatement().FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<Graphql.Types.User?> ReadGraphqlUser(int id)
    {
        var user = await Read(id);
        if (user is null) {  return null; }
        return _mapper.mapFrom(user);
    }

    public async Task<Graphql.Types.User?> ReadGraphqlUser(string username)
    {
        var user = await Read(username);
        if (user is null) {  return null; }
        return _mapper.mapFrom(user);
    }

    public async Task<UserResult> ReadUser(UserInput input)
    {
        var user = await ReadGraphqlUser(input.UserId);
        return new UserResult(user);
    }

    public async Task<UserByUserNameResult> ReadUser(UserByUserNameInput input)
    {
        var user = await this.PreparedStatement().FirstOrDefaultAsync(u => u.Username == input.UserName);
        if (user is null) {  return new UserByUserNameResult(null); }
        return new UserByUserNameResult(_mapper.mapFrom(user));
    }

    public async Task<UsersResult> ReadUsers(UsersInput input)
    {
        IQueryable<User> usersQuery = PreparedStatement();

        if (input.Filter is not null && input.Filter.Length > 0)
            usersQuery = usersQuery.Where(u => u.Username.ToLower().Contains(input.Filter.ToLower()) || u.FirstName.ToLower().Contains(input.Filter.ToLower())|| u.LastName.ToLower().Contains(input.Filter.ToLower()));

        usersQuery = input.Direction switch
        {
            SortDirection.ASC => input.Sorting switch
            {
                SortUsers.ID => usersQuery.OrderBy(u => u.Id),
                SortUsers.USERNAME => usersQuery.OrderBy(u => u.Username).ThenBy(u => u.Id),
                SortUsers.FIRST_NAME => usersQuery.OrderBy(u => u.FirstName).ThenBy(u => u.Id),
                SortUsers.LAST_NAME => usersQuery.OrderBy(u => u.LastName).ThenBy(u => u.Id),
                SortUsers.FOLLOWER => usersQuery.OrderBy(u => u.Follower.Count).ThenBy(u => u.Id),
                _ => throw new ArgumentOutOfRangeException()
            },
            SortDirection.DSC => input.Sorting switch
            {
                SortUsers.ID => usersQuery.OrderByDescending(u => u.Id),
                SortUsers.USERNAME => usersQuery.OrderByDescending(u => u.Username).ThenByDescending(u => u.Id),
                SortUsers.FIRST_NAME => usersQuery.OrderByDescending(u => u.FirstName).ThenByDescending(u => u.Id),
                SortUsers.LAST_NAME => usersQuery.OrderByDescending(u => u.LastName).ThenByDescending(u => u.Id),
                SortUsers.FOLLOWER => usersQuery.OrderByDescending(u => u.Follower.Count).ThenByDescending(u => u.Id),
                _ => throw new ArgumentOutOfRangeException()
            },
            _ => usersQuery
        };
        var count = await usersQuery.CountAsync();

        usersQuery = usersQuery.Skip(input.Offset).Take(input.Limit);

        var users = await usersQuery.ToListAsync();
        
        return new UsersResult(users.Select(u => _mapper.mapFrom(u)).ToList(), count);
    }

    public Task<bool> Exists(int userId) => Table.AnyAsync(u => u.Id == userId);

}