using Model.Entities;

namespace Domain.DTOs;

public record DefaultUserDto(int Id, string Username, string FirstName, string LastName);

public record CountUserDto(int Count, IQueryable<DefaultUserDto> Users);

public record FullUserDto(int Id, string Username, string FirstName, string LastName, int FollowerCount, int FollowingCount, List<DefaultUserDto>? Followers, List<DefaultUserDto>? Followings);