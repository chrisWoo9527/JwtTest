using Identity.Sql.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.NetCore_Jwt
{
    public class CheckJwtVersionFilter : IAsyncActionFilter
    {
        private readonly UserManager<User> userManager;
        private readonly IMemoryCache memoryCache;

        public CheckJwtVersionFilter(UserManager<User> userManager, IMemoryCache memoryCache)
        {
            this.userManager = userManager;
            this.memoryCache = memoryCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool checkJwtVersionattribute = false;
            if (context.ActionDescriptor is ControllerActionDescriptor)
            {
                ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
                checkJwtVersionattribute = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(CheckJwtVersion), false).Any();
            }

            if (!checkJwtVersionattribute)
            {
                await next();
                return;
            }


            var claimUserName = context.HttpContext.User.FindFirst("UserName");

            string cacheKey = $"{claimUserName!.Value}";
            User user = await memoryCache.GetOrCreateAsync(cacheKey, async e =>
            {
                int expricetimes = Random.Shared.Next(5, 10);
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5);
                return await userManager.FindByNameAsync(claimUserName.Value);
            });

            if (user.JwtVersion > Convert.ToInt64(context.HttpContext.User.FindFirst("JwtVersion").Value))
            {
                ObjectResult objectResult = new ObjectResult("令牌失效")
                {
                    StatusCode = 401
                };
                context.Result = objectResult;
                return;
            }
            else
            {
                await next();
                return;
            }
        }
    }
}
