using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    Task<TagResult> ReadTag(TagInput input);

    Task<TagsResult> ReadTags(TagsInput input);
    Task<List<Graphql.Types.Tag>> ReadAllGraphqlTag();

    Task<bool> ExistByIds(List<int> ids);
    Task<bool> Exists(int tagId);
}