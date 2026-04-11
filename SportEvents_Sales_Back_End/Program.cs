using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.DatabaseAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//adding Entity Framework:
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Default")));

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

app.UseAuthorization();
app.MapControllers();
app.Run();
