using Model.Entities;

namespace Domain.DTOs;

public record UserInfoDto(int Id, string Username, string FirstName, string LastName);

public record UserDto(int Id, string Username, string FirstName, string LastName, int FollowerCount, int FollowingCount, List<UserInfoDto>? Followers, List<UserInfoDto>? Followings);