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

public class IsUserHandler : AuthorizationHandler<IsUserRequirement>
{

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext authContext, IsUserRequirement requirement)
    {
        var userid = authContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userid is null)
            return Task.CompletedTask;
        
        Console.WriteLine("hehe");
        
        authContext.Succeed(requirement); 
        return Task.CompletedTask;
    }
}