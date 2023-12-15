using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Backend.Identity;

public interface IUserService {
    Task<User?> GetUser(string? username);
    bool IsAuthenticated(string password, User user);
    string HashPassword(string password);
}

public sealed class UserService : IUserService {
    private readonly YDbContext _db;

    public UserService(YDbContext db) {
        _db = db;
    }

    public async Task<User?> GetUser(string? username) {
        ArgumentNullException.ThrowIfNull(username);

        return await _db.Users.SingleOrDefaultAsync(u => u.Username == username);
    }

    public bool IsAuthenticated(string password, User u) {
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(u);

        return BCrypt.Net.BCrypt.Verify(password, u.PasswordHash);
    }

    public string HashPassword(string password) {
        ArgumentNullException.ThrowIfNull(password);

        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}