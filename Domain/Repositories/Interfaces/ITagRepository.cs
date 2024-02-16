using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    Task<Graphql.Types.Tag?> ReadTag(int id);

    Task<List<Graphql.Types.Tag>> ReadTags();
    
}