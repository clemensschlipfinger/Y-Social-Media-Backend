using Domain.Graphql.Types;
using Mapster;
using Yeet = Model.Entities.Yeet;

namespace Domain.Mapper;

public class MapYeet : FromMapper<Yeet, Graphql.Types.Yeet>
{
    public Graphql.Types.Yeet mapFrom(Yeet yeet)
    {
        return new Graphql.Types.Yeet()
        {
            Id = yeet.Id,
            Title = yeet.Title,
            Body = yeet.Body,
            CreatedAt = yeet.CreatedAt,
            Likes = yeet.Likes,
            UserId = yeet.UserId,
            User = yeet.User.Adapt<Graphql.Types.User>(),
            Yomments = yeet.Yomments.Select(y => y.Adapt<Graphql.Types.Yomment>()).ToList(),
            Tags = yeet.Tags.Select(y => y.Tag.Adapt<Graphql.Types.Tag>()).ToList()
        };
    }
}