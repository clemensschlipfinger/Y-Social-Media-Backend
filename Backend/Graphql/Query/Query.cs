using Domain.DTOs;
using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Implementations;
using Domain.Repositories.Interfaces;
using Domain.Services.Implementations;
using Domain.Services.Interfaces;
using HotChocolate.Authorization;
using Mapster;
using Model.Entities;

namespace Backend.Graphql.Query;

public class Query {
    
    public async Task<UsersResult> Users(UsersInput input, IUserService userService) => await userService.Users(input);
    
    public async Task<UserResult> User(UserInput input, IUserService userService ) => await userService.User(input);
    
    /*
    public async Task<YeetsResult> Yeets(YeetsInput input) => await _yeetService.Yeets(input);
    
    [Error(typeof(YeetNotFoundException))]
    public async Task<YeetResult> Yeet(YeetInput input) => await _yeetService.Yeet(input);
    
    public async Task<FeedResult> Feed(FeedInput input) => await _yeetService.Feed(input);
    
    public async Task<YommentsResult> Yomments(YommentsInput input) => await _yommentService.Yomments(input);
    
    [Error(typeof(YommentNotFoundException))]
    public async Task<YommentResult> Yomment(YommentInput input) => await _yommentService.Yomment(input);
    
    */
    public async Task<TagsResult> Tags(TagsInput input, ITagService tagService) => await tagService.Tags(input);
    
    public async Task<TagResult> Tag(TagInput input, ITagService tagService) => await tagService.Tag(input);
}