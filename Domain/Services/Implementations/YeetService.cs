using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Mapster;
using Model.Entities;

namespace Domain.Services.Implementations;

public class YeetService : IYeetService
{
    private IYeetRepository _yeetRepository;
    private IUserRepository _userRepository;
    private ITagRepository _tagRepository;

    public YeetService(IYeetRepository yeetRepository, IUserRepository userRepository, ITagRepository tagRepository)
    {
        _yeetRepository = yeetRepository;
        _userRepository = userRepository;
        _tagRepository = tagRepository;
    }

    public Task<YeetsResult> Yeets(YeetsInput input)
    {
        return _yeetRepository.ReadYeets(input);
    }

    public Task<YeetResult> Yeet(YeetInput input)
    {
        return _yeetRepository.ReadYeet(input);
    }

    public Task<FeedResult> Feed(FeedInput input)
    {
        return _yeetRepository.ReadFeed(input);
    }

    public async Task<CreateYeetResult> CreateYeet(CreateYeetInput input)
    {
        if (!await _userRepository.Exists(input.UserId))
            throw new UserNotFoundException(input.UserId);
        if (!await _tagRepository.ExistByIds(input.Tags))
            throw new TagNotFoundException(input.Tags[0]);
        
        var yeet = new Yeet()
        {
            Title = input.Title,
            UserId = input.UserId,
            Body = input.Body,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            Tags = input.Tags.Select(t => new YeetHasTags() { TagId = t}).ToList()
        };

        yeet = await _yeetRepository.CreateAsync(yeet);

        var graphQlYeet = await _yeetRepository.ReadGraphqlYeet(yeet.Id);
        return new CreateYeetResult(graphQlYeet);
    }

    public async Task<DeleteYeetResult> DeleteYeet(DeleteYeetInput input)
    {
        var yeet = await _yeetRepository.ReadAsync(input.YeetId);
        if (yeet is null)
            throw new YeetNotFoundException(input.YeetId);

        await _yeetRepository.DeleteAsync(yeet);
        return new DeleteYeetResult(input.YeetId);
    }

    public async Task<AddLikeToYeetResult> AddLikeToYeet(AddLikeToYeetInput input)
    {
        var yeet = await _yeetRepository.ReadAsync(input.YeetId);
        if (yeet is null)
            throw new YeetNotFoundException(input.YeetId);
        
        yeet.Likes += 1;

        await _yeetRepository.UpdateAsync(yeet);
        return new AddLikeToYeetResult(yeet.Likes);
    }
}