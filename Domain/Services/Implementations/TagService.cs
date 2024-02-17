using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations;

public class TagService : ITagService
{
    public async Task<TagsResult> Tags(TagsInput input)
    {
        throw new NotImplementedException();
    }

    public async Task<TagResult> Tag(TagInput input)
    {
        throw new NotImplementedException();
    }

    public async Task<CreateTagResult> CreateTag(CreateTagInput input)
    {
        throw new NotImplementedException();
    }
}