using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Mapster;
using Model.Entities;

namespace Domain.Services.Implementations;

public class TagService : ITagService
{
    /*
    private readonly ITagRepository _tagRepository;
    private readonly IRegexService _regexService;

    public TagService(ITagRepository tagRepository, IRegexService regexService)
    {
        _tagRepository = tagRepository;
        _regexService = regexService;
    }

    public async Task<TagsResult> Tags(TagsInput input)
    {
        var tags = await _tagRepository.ReadTags();
        tags = tags.Where(t => _regexService.Matches(input.Filter, t.Name)).ToList();
        tags.Sort(delegate(Graphql.Types.Tag x, Graphql.Types.Tag y)
        {
            switch (input.Sorting)
            {
                case SortTags.NAME:
                    if (x.Name == null && y.Name == null) return 0;
                    if (x.Name == null) return -1;
                    if (y.Name == null) return 1;
                    return x.Name.CompareTo(y.Name);

                case SortTags.ID:
                default:
                    if (x.Id == 0 && y.Id == 0) return 0;
                    if (x.Id == 0) return -1;
                    if (y.Id == 0) return 1;
                    return x.Id.CompareTo(y.Id);
            }
        });
        if (input.Direction == SortDirection.DSC) tags.Reverse();
        var count = tags.Count;
        tags = tags.Skip(input.Offset).Take(input.Limit).ToList();
        return new TagsResult(tags.Select(t => t.Adapt<Graphql.Types.Tag>()).ToList(), count);
    }

    public async Task<TagResult> Tag(TagInput input)
    {
        var tag = await _tagRepository.ReadTag(input.TagId);
        if (tag == null) throw new TagNotFoundException(input.TagId); 
        return new TagResult(tag);
    }

    public async Task<CreateTagResult> CreateTag(CreateTagInput input)
    {
        var tag = new Tag()
        {
            Name = input.Name
        };
        var result = await _tagRepository.CreateAsync(tag);
        return new CreateTagResult(result.Adapt<Graphql.Types.Tag>(), await _tagRepository.ReadTags());
    }
    */
    public Task<TagsResult> Tags(TagsInput input)
    {
        throw new NotImplementedException();
    }

    public Task<TagResult> Tag(TagInput input)
    {
        throw new NotImplementedException();
    }

    public Task<CreateTagResult> CreateTag(CreateTagInput input)
    {
        throw new NotImplementedException();
    }
}