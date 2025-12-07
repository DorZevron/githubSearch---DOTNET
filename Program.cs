using githubSearch.Api.Services;
using githubSearch.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

//---------------
// Program.cs - Application Setup
//---------------

var builder = WebApplication.CreateBuilder(args);


//---------------
// Services
//---------------

// Session saveing in server Memory Ram 
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// for accessing HttpContext in services
builder.Services.AddHttpContextAccessor();


// My Services
builder.Services.AddScoped<GithubService>();
builder.Services.AddScoped<BookmarkService>();


//---------------
// JWT Authentication
//---------------
var key = Encoding.ASCII.GetBytes("SECRET_KEY_123456789");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // false for https
    options.SaveToken = true; // ot get token and use it later

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, // validate the signing key
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, //not use issuer
        ValidateAudience = false //not use audience
    };
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//---------------
// Middleware oder is Important
//---------------

app.UseSwagger();
app.UseSwaggerUI();

// Routing system
app.UseHttpsRedirection();
app.UseRouting();

//Session must be before Authentication and Authorization
app.UseSession();

app.UseMiddleware<JwtValidationMiddleware>(); // Custom JWT Validation Middleware
app.UseAuthentication(); // JwtBearer Action
app.UseAuthorization(); // [Authorize] Action
app.MapControllers();

app.Run();


