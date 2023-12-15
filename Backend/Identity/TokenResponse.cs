using Model.Entities;

namespace Backend.Identity;

public record TokenResponse(string token, User user);