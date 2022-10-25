using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Noghte.BuildingBlock;
using Noghte.BuildingBlock.ApiResponses;
using Noghte.BuildingBlock.ConsumerMessages;
using Noghte.Domain.Users;

namespace Noghte.Infrastructure.Users;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizationAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (User)context.HttpContext.Items["User"];
        if (user == null)
        {
            // not logged in
            context.Result = new JsonResult(new ConsumerRejected { Message = ConsumerMessage.ACCESS_DENDIED(), StatusCode = ConsumerStatusCode.UnAuthorized });
            context.HttpContext.Response.StatusCode = 401;
        }
    }
}
