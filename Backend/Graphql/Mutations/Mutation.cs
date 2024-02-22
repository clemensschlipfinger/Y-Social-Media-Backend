using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Implementations;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using HotChocolate.Authorization;
using HotChocolate.Resolvers;
using Model.Entities;
using IUserService = Domain.Services.Interfaces.IUserService;

namespace Backend.Graphql.Mutations;

public class Mutation
{

    [Error(typeof(UsernameAlreadyExistsException))]
    [Error(typeof(PasswordTooShortException))]
    public async Task<RegistrationResult> Registration(RegistrationInput input, IUserService userService) =>
        await userService.Registration(input);


    [Error(typeof(UsernameNotFoundException))]
    [Error(typeof(InvalidPasswordException))]
    public async Task<LoginResult> Login(LoginInput input, IUserService userService) => await userService.Login(input);

    [Authorize]
    [Error(typeof(UserNotFoundException))]
    [Error(typeof(FollowingNotFoundException))]
    [Error(typeof(AlreadyFollowingException))]
    public async Task<AddFollowResult> AddFollow([GlobalState("UserId")] int userId, AddFollowInput input,
        IUserService userService)
    {
        input.UserId = userId;
        return await userService.AddFollow(input);
    }

    [Authorize]
    [Error(typeof(UserNotFoundException))]
    [Error(typeof(FollowingNotFoundException))]
    public async Task<RemoveFollowResult> RemoveFollow([GlobalState("UserId")] int userId, RemoveFollowInput input,
        IUserService userService)
    {
        input.UserId = userId;
        return await userService.RemoveFollow(input);
    }

/*
    [Authorize]
    [Error(typeof(UserNotFoundException))]
    [Error(typeof(TagNotFoundException))]
    public async Task<CreateYeetResult> CreateYeet(CreateYeetInput input) => await _yeetService.CreateYeet(input);

    [Authorize]
    [Error(typeof(YeetNotFoundException))]
    public async Task<DeleteYeetResult> DeleteYeet(DeleteYeetInput input) => await _yeetService.DeleteYeet(input);

    [Authorize]
    [Error(typeof(YeetNotFoundException))]
    [Error(typeof(UserNotFoundException))]
    public async Task<CreateYommentResult> CreateYomment(CreateYommentInput input) =>
        await _yommentService.CreateYomment(input);

    [Authorize]
    [Error(typeof(YommentNotFoundException))]
    public async Task<DeleteYommentResult> DeleteYomment(DeleteYommentInput input) =>
        await _yommentService.DeleteYomment(input);
        */

    [Authorize]
    [Error(typeof(TagAlreadyExistsException))]
    public async Task<CreateTagResult> CreateTag(CreateTagInput input, ITagService tagService) => await tagService.CreateTag(input);
}