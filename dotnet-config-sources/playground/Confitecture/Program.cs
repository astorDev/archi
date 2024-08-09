using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("Db"))
);

var app = builder.Build();

app.MapGet("/", async (DbContext context) => {
    await context.Database.OpenConnectionAsync();
    return "Connected!";
});

app.Run();