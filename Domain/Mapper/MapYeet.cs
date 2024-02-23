using Domain.Graphql.Types;
using Mapster;
using User = Model.Entities.User;
using Yeet = Model.Entities.Yeet;
using Yomment = Model.Entities.Yomment;

namespace Domain.Mapper;

public class MapYeet : FromMapper<Yeet, Graphql.Types.Yeet>
{
    private readonly FromMapper<Yomment, Graphql.Types.Yomment> _yommentMapper;
    private readonly FromMapper<User, Graphql.Types.User> _userMapper;

    public MapYeet(FromMapper<Yomment, Graphql.Types.Yomment> yommentMapper, FromMapper<User, Graphql.Types.User> userMapper)
    {
        this._yommentMapper = yommentMapper;
        _userMapper = userMapper;
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
            User = _userMapper.mapFrom(yeet.User),
            Yomments = yeet.Yomments.Select(y => _yommentMapper.mapFrom(y)).ToList(),
            Tags = yeet.Tags.Select(y => y.Tag.Adapt<Graphql.Types.Tag>()).ToList()
        };
    }
}