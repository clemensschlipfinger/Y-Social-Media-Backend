using System.Security.Claims;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;

namespace Backend.Graphql;

public class GetUserIdInterceptor: DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(HttpContext context,
        IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken)
    {
        string userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if(userId is not null)
            requestBuilder.SetProperty("UserId", int.Parse(userId));

        return base.OnCreateAsync(context, requestExecutor, requestBuilder,
            cancellationToken);
    }
}
