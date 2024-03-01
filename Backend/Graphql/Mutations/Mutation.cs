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
    [Error(typeof(PasswordHasNoUpperCaseLettersException))]
    [Error(typeof(PasswordHasNoLowerCaseLettersException))]
    [Error(typeof(PasswordHasNoDigitsException))]
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
    [Error(typeof(NeverFollowedException))]
    public async Task<RemoveFollowResult> RemoveFollow([GlobalState("UserId")] int userId, RemoveFollowInput input,
        IUserService userService)
    {
        input.UserId = userId;
        return await userService.RemoveFollow(input);
    }

    [Authorize]
    [Error(typeof(UserNotFoundException))]
    [Error(typeof(TagNotFoundException))]
    public async Task<CreateYeetResult> CreateYeet([GlobalState("UserId")] int userId, CreateYeetInput input,
        IYeetService yeetService)
    {
        input.UserId = userId;
        return await yeetService.CreateYeet(input);  
    } 

    [Authorize]
    [Error(typeof(YeetNotFoundException))]
    public async Task<DeleteYeetResult> DeleteYeet(DeleteYeetInput input, IYeetService yeetService) => await yeetService.DeleteYeet(input);

    [Authorize]
    [Error(typeof(YeetNotFoundException))]
    [Error(typeof(UserNotFoundException))]
    public async Task<CreateYommentResult> CreateYomment([GlobalState("UserId")] int userId, CreateYommentInput input, IYommentService yommentService)
    {
        input.UserId = userId;
        return await yommentService.CreateYomment(input);
    }
    [Authorize]
    [Error(typeof(YeetNotFoundException))]
    public async Task<AddLikeToYeetResult> AddLikeToYeet(AddLikeToYeetInput input, IYeetService yeetService) =>
        await yeetService.AddLikeToYeet(input);

    [Authorize]
    [Error(typeof(YommentNotFoundException))]
    public async Task<DeleteYommentResult> DeleteYomment(DeleteYommentInput input,IYommentService yommentService) =>
        await yommentService.DeleteYomment(input);

    [Authorize]
    [Error(typeof(TagAlreadyExistsException))]
    public async Task<CreateTagResult> CreateTag(CreateTagInput input, ITagService tagService) => await tagService.CreateTag(input);

    [Authorize]
    [Error(typeof(YommentNotFoundException))]
    public async Task<AddLikeToYommentResult> AddLikeToYomment(AddLikeToYommentInput input, IYommentService yommentService) =>
        await yommentService.AddLikeToYomment(input);
}
