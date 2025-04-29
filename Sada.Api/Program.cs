using Microsoft.EntityFrameworkCore;
using Sada.Api.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TasksInMemory"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
