
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;



namespace githubSearch.Api.Middleware;
//---------------
// JwtValidationMiddleware.cs - JWT Validation Middleware
//---------------

public class JwtValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _key = "SUPER_SUPER_SECRET_KEY_123456789";


    private readonly string[] _protectedPaths = new[]
    {
        "/api/github",
        "/api/bookmarks"
    };



    public JwtValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }




    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower();

        // Check if the request path is protected
        if (path !=null && _protectedPaths.Any(p => path.StartsWith(p)))
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("JWT token missing");
                return;
            }

            try{
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_key);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
            }
            catch(Exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid or expired JWT token");
                return;
            }
        }
        else{
            await _next(context);
            return;
        }

        await _next(context);
    }
}