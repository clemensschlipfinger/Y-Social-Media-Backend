using Domain.DTOs;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Implementations;
using Domain.Repositories.Interfaces;
using HotChocolate.Authorization;
using Mapster;
using Model.Entities;

namespace Backend.Graphql.Query;

public class Query {
    public UsersResult Users(UsersInput input)
    {
        // Implement your logic here
        throw new NotImplementedException();
    }

    public UserResult User(UserInput input)
    {
        // Implement your logic here
        throw new NotImplementedException();
    }

    public YeetsResult Yeets(YeetsInput input)
    {
        // Implement your logic here
        throw new NotImplementedException();
    }

    public YeetResult Yeet(YeetInput input)
    {
        // Implement your logic here
        throw new NotImplementedException();
    }

    public FeedResult Feed(FeedInput input)
    {
        // Implement your logic here
        throw new NotImplementedException();
    }

    public YommentsResult Yomments(YommentsInput input)
    {
        // Implement your logic here
        throw new NotImplementedException();
    }

    public YommentResult Yomment(YommentInput input)
    {
        // Implement your logic here
        throw new NotImplementedException();
    }

    public TagsResult Tags(TagsInput input)
    {
        // Implement your logic here
        throw new NotImplementedException();
    }

    public TagResult Tag(TagInput input)
    {
        // Implement your logic here
        throw new NotImplementedException();
    }
}