using Mapster;
using Yomment = Model.Entities.Yomment;
using User = Model.Entities.User;

namespace Domain.Mapper;

public class MapYomment : FromMapper<Yomment, Graphql.Types.Yomment>
{
    private readonly FromMapper<User, Graphql.Types.User> _userMapper;

    public MapYomment(FromMapper<User, Graphql.Types.User> userMapper)
    {
        _userMapper = userMapper;
    }


    public Graphql.Types.Yomment mapFrom(Yomment f)
    {
        return new Graphql.Types.Yomment()
        {
            Id = f.Id,
            Body = f.Body,
            Likes = f.Likes,
            CreatedAt = f.CreatedAt.ToLocalTime(),
            User = _userMapper.mapFrom(f.User)
        };
    }
}