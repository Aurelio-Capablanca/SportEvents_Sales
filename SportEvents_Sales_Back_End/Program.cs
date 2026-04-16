using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
// OpenAPI
builder.Services.AddOpenApi();

//Entity Framework
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Default")));
//sign in new Services-like classes (? - weird)
builder.Services.AddScoped<LoginSessions>();
builder.Services.AddScoped<JWTIssuer>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ehcl",
            ValidAudience = "front-end-sportsales",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("4b87b5d498a4b88462abd129d47179128809b05fc2a470ed29ffbd47b6af525a")
            )
        };
    });

builder.Services.AddAuthorization();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapGet("/ping", () => "pong");

//test DB connectivity
app.MapGet("/health/db", async (AppDbContext db) =>
{
    var ok = await db.Database.CanConnectAsync();
    return ok ? Results.Ok("DB OK") : Results.Problem("DB FAIL");
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
