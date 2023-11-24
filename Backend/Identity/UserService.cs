using Model.Entities;

namespace Backend.Identity;

public interface IUserService
{
    Task<User?> GetUser(string? username);
    bool IsAuthenticated(string? password, User? user);
}
/*
public sealed class UserService : IUserService
{
    private readonly BloggingContext _db;

    public UserService(BloggingContext db)
    {
        _db = db;
    }

    public async Task<User?> GetUser(string? username)
    {
        ArgumentNullException.ThrowIfNull(username);

        return await _db.Users.SingleOrDefaultAsync(u => u.Username == username);
    }

    public bool IsAuthenticated(string? password, User u)
    {
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(u);

        return BCrypt.Net.BCrypt.Verify(password, u.PasswordHash);
    }
}
*/