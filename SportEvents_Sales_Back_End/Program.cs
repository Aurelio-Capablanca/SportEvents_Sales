using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;
using SportEvents_Sales_Back_End.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SportEvents_Sales_Back_End.Domain.Business;
using SportEvents_Sales_Back_End.Domain.Rules;

/* 
 Tasks list : 
   - FrontEnd Basics (MVVC)
        * public site
        * private site
   - Dual Login (Client and Admin) -> generate Roles over it (logically)
   - CRUD's   (Ingestion -> Validation -> Persistence)
       * Clientes  (Aurel)
       * Tickets  + Orden (Aurel) 
       * Partidos
       * Precios 
       * Sedes
       * 
   - Test Scrapping (Aurel)
     
*/

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
// OpenAPI
builder.Services.AddOpenApi();
//Entity Framework
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Default")));
//Security
builder.Services.AddScoped<LoginSessions>();
builder.Services.AddScoped<JWTIssuer>();
builder.Services.AddScoped<HumanVerification>();
builder.Services.AddHttpContextAccessor();
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

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddScoped<IUserSessionProvider, SessionProvider>();
//Client
builder.Services.AddScoped<ClientLogic>();
builder.Services.AddScoped<ClientRules>();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
//ping tester
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
