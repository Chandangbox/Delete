using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RoomService.WebAPI
{
    public class PasswordCheckerMiddleware
    {
        private readonly RequestDelegate _next;
        public PasswordCheckerMiddleware(RequestDelegate next)
        {
			 _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
			if (!context.Request.Headers.Keys.Contains("passwordKey"))
            {
                context.Response.StatusCode = 400; //Bad Request                
                await context.Response.WriteAsync("User Key is missing");
                return;
            }
            else
            {
				string value = context.Request.Headers["passwordKey"];
                if(value !="passwordKey123456789")
                {
                    context.Response.StatusCode = 401; //UnAuthorized
                    await context.Response.WriteAsync("Invalid User Key");
                    return;
                }
            }

            await _next.Invoke(context);
        
        }
    }
}
