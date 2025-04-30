using Microsoft.EntityFrameworkCore;
using Sada.Api.Common;
using Sada.Api.Data;
using Sada.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddInMemory();
builder.AddDocumentation();
builder.AddInMemory();
builder.AddServices();

var app = builder.Build();

app.MapEndpoint();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();
app.Run();
