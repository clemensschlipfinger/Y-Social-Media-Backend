using System.Security.Claims;
using HotChocolate.Resolvers;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Identity.Policies;

public class OwnsBlogRequirement : IAuthorizationRequirement { } 

/*
public class OwnsBlogHandler : AuthorizationHandler<OwnsBlogRequirement>
{
    public readonly BloggingContext _db;

    public OwnsBlogHandler(BloggingContext db)
    {
        this._db = db;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, OwnsBlogRequirement requirement)
    {
        var reqblogId = (int)(context.Resource as IMiddlewareContext)?.Selection.Arguments["id"].Value;
        if (reqblogId == null)
        {
            return Task.CompletedTask; 
        }
        
        var userid = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userid is null)
        {
            return Task.CompletedTask;
        }
        var foundblog = _db.Blogs.Where(b => b.UserId.ToString() == userid).SingleOrDefault(b => b.Id == reqblogId);
        if (foundblog is null)
        {
            return Task.CompletedTask;
        }
        
        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

*/