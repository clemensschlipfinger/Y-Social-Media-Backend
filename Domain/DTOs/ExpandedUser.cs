
using Model.Entities;

namespace Domain.DTOs;

public class ExpandedUser : User
{
    public int FollowingCount { get; set; }
    public int FollowerCount { get; set; }
    public List<DefaultUserDto>? Followers { get; set; }
    public List<DefaultUserDto>? Followings { get; set; }
}