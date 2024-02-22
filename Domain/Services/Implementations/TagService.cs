using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Mapster;
using Model.Entities;
using Npgsql;

namespace Domain.Services.Implementations;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public Task<TagsResult> Tags(TagsInput input)
    {
        return _tagRepository.ReadTags(input);
    }

    public Task<TagResult> Tag(TagInput input)
    {
        return _tagRepository.ReadTag(input);
    }

    public async Task<CreateTagResult> CreateTag(CreateTagInput input)
    {
        if ((await _tagRepository.ReadAsync(t => t.Name == input.Name)).Any())
            throw new TagAlreadyExistsException(input.Name);
        
        var tag = new Tag() { Name = input.Name };
        var result = await _tagRepository.CreateAsync(tag);
        return new CreateTagResult(result.Adapt<Graphql.Types.Tag>(), await _tagRepository.ReadAllGraphqlTag());
    }
}