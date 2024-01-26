using System.Security.Claims;
using HotChocolate.Authorization;
using HotChocolate.Resolvers;
using Microsoft.AspNetCore.Authorization;
using IAuthorizationHandler = HotChocolate.Authorization.IAuthorizationHandler;

namespace Backend.Identity.Policies;

public class IsUserRequirement : IAuthorizationRequirement
{
    public readonly string _idParam;

    public IsUserRequirement(string idParam)
    {
        _idParam = idParam;
    } 
} 

public class IsUserHandler : AuthorizationHandler<IsUserRequirement, IResolverContext>
{

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext authContext, IsUserRequirement requirement, IResolverContext context)
    {
        var reqblogId = (context as IMiddlewareContext)?.Selection.Arguments["input"];
        
        var userid = authContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
        if (userid is null)
            return Task.CompletedTask;
        
        
        authContext.Succeed(requirement); 
        return Task.CompletedTask;
    }
}