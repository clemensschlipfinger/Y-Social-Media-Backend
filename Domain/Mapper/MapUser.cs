using Domain.Graphql.Types;
using Mapster;
using User = Model.Entities.User;

namespace Domain.Mapper;

public class MapUser : FromMapper<User, Graphql.Types.User>
{
    public Graphql.Types.User mapFrom(User f)
    {
        return new Graphql.Types.User()
        {
            Id = f.Id,
            Username = f.Username,
            FirstName = f.FirstName,
            LastName = f.LastName,
            FollowerCount = f.Follower.Count,
            FollowingCount = f.Following.Count,
            Follower = f.Follower.Select(f => f.Follower.Adapt<UserInfo>()).ToList(),
            Following = f.Following.Select(f => f.Following.Adapt<UserInfo>()).ToList()
        };
    }
}