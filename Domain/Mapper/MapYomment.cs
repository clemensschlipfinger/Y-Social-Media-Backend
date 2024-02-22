using Domain.Graphql.Types;
using Mapster;
using Yomment = Model.Entities.Yomment;

namespace Domain.Mapper;

public class MapYomment : FromMapper<Yomment, Graphql.Types.Yomment>
{
    public Graphql.Types.Yomment mapFrom(Yomment f)
    {
        return new Graphql.Types.Yomment()
        {
            Id = f.Id,
            Body = f.Body,
            Likes = f.Likes,
            CreatedAt = f.CreatedAt.ToLocalTime(),
            User = f.User.Adapt<UserInfo>()
        };
    }
}