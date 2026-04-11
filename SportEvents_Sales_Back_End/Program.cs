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

//tester!
app.MapGet("/users", async (AppDbContext db) =>
{
    return await db.Users.ToListAsync();
});


app.UseAuthorization();
app.MapControllers();
app.Run();
