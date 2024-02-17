using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;

namespace Domain.Services.Interfaces;

public interface ITagService
{
    Task<TagsResult> Tags(TagsInput input);
    Task<TagResult> Tag(TagInput input);
    Task<CreateTagResult> CreateTag(CreateTagInput input);
}