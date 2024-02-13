
using Model.Entities;

namespace Domain.DTOs;

public class ExpandedUser : User
{
    public int FollowingCount { get; set; }
    public int FollowerCount { get; set; }
    public IQueryable<DefaultUserDto> Followers { get; set; }
    public IQueryable<DefaultUserDto> Following { get; set; }
}