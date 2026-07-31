using System.Text;
using E_Commerce_Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_Commerce_API.Attributes
{
    public class RedisCasheAttribute : ActionFilterAttribute
    {
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get Cashe service from DI container
            var casheService = context.HttpContext.RequestServices.GetRequiredService<ICasheService>();
            
            // 
            var casheKey = CreateCasheKey(context.HttpContext.Request);
            var cached = await casheService.GetAsync(casheKey);

            if(!string.IsNullOrEmpty(cached))
            {
                var contentResult = new ContentResult
                {
                    Content = cached,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                context.Result = contentResult;
                return;
            }

            // if not exists in cache, execute the action and cache the result

            var execute = await next.Invoke();
            if(execute.Result is OkObjectResult { Value: not null}ok)
                await casheService.SetAsync(casheKey, ok.Value, TimeSpan.FromSeconds(1000));
            return;
        }

        private static string CreateCasheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path).Append("?");

            foreach (var (k, value) in request.Query.OrderBy(x => x.Key))
            {
                key.Append(k).Append("=").Append(value).Append("&");
            }

            return key.ToString();
        }
    }
}
