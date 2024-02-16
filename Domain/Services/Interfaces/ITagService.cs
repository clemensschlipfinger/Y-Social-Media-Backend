using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;

namespace Domain.Services.Interfaces;

public interface ITagService
{
    TagsResult Tags(TagsInput input);
    TagResult Tag(TagInput input);
    CreateTagResult CreateTag(CreateTagInput input);
}