using Domain.Graphql.Types;
using Mapster;
using Yeet = Model.Entities.Yeet;
using Yomment = Model.Entities.Yomment;

namespace Domain.Mapper;

public class MapYeet : FromMapper<Yeet, Graphql.Types.Yeet>
{
    private readonly FromMapper<Yomment, Graphql.Types.Yomment> _yommentMapper;

    public MapYeet(FromMapper<Yomment, Graphql.Types.Yomment> yommentMapper)
    {
        this._yommentMapper = yommentMapper;
    }

    public Graphql.Types.Yeet mapFrom(Yeet yeet)
    {
        return new Graphql.Types.Yeet()
        {
            Id = yeet.Id,
            Title = yeet.Title,
            Body = yeet.Body,
            CreatedAt = yeet.CreatedAt.ToLocalTime(),
            Likes = yeet.Likes,
            User = yeet.User.Adapt<Graphql.Types.UserInfo>(),
            Yomments = yeet.Yomments.Select(y => _yommentMapper.mapFrom(y)).ToList(),
            Tags = yeet.Tags.Select(y => y.Tag.Adapt<Graphql.Types.Tag>()).ToList()
        };
    }
}