var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var dbConnectionString = app.Configuration.GetConnectionString("Db");
app.Logger.LogInformation("Db Connection string: {dbConnectionString}", dbConnectionString);

app.Run();
