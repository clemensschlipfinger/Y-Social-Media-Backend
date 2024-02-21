using Domain.DTOs;
using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Implementations;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using HotChocolate.Authorization;
using Mapster;
using Model.Entities;

namespace Backend.Graphql.Query;

public class Query {
    private readonly IUserService _userService;
    private readonly IYeetService _yeetService;
    private readonly IYommentService _yommentService;
    private readonly ITagService _tagService;

    public Query(IUserService userService, IYeetService yeetService, IYommentService yommentService, ITagService tagService)
    {
        _userService = userService;
        _yeetService = yeetService;
        _yommentService = yommentService;
        _tagService = tagService;
    }
    
    public async Task<UsersResult> Users(UsersInput input) => await _userService.Users(input);
    
    [Error(typeof(UserNotFoundException))]
    public async Task<UserResult> User(UserInput input) => await _userService.User(input);
    
    public async Task<YeetsResult> Yeets(YeetsInput input) => await _yeetService.Yeets(input);
    
    [Error(typeof(YeetNotFoundException))]
    public async Task<YeetResult> Yeet(YeetInput input) => await _yeetService.Yeet(input);
    
    public async Task<FeedResult> Feed(FeedInput input) => await _yeetService.Feed(input);
    
    public async Task<YommentsResult> Yomments(YommentsInput input) => await _yommentService.Yomments(input);
    
    [Error(typeof(YommentNotFoundException))]
    public async Task<YommentResult> Yomment(YommentInput input) => await _yommentService.Yomment(input);
    
    public async Task<TagsResult> Tags(TagsInput input) => await _tagService.Tags(input);
    
    [Error(typeof(TagNotFoundException))]
    public async Task<TagResult> Tag(TagInput input) => await _tagService.Tag(input);
}