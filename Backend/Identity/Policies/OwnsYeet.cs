using System.Security.Claims;
using HotChocolate.Resolvers;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Identity.Policies;

public class IsUserRequirement : IAuthorizationRequirement
{
    private readonly string _idParam;

    public IsUserRequirement(string idParam)
    {
        _idParam = idParam;
    } 
} 

public class OwnsBlogHandler : AuthorizationHandler<IsUserRequirement>
{

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, IsUserRequirement requirement)
    {
        
        var req_id = (int)(context.Resource as IMiddlewareContext)?.Selection.Arguments["id"].Value;
        if (req_id == null)
            return Task.CompletedTask; 
        
        var userid = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userid is null)
            return Task.CompletedTask;
        
        if(userid != req_id.ToString())
            return Task.CompletedTask;
        
        context.Succeed(requirement); 
        return Task.CompletedTask;
    }
}