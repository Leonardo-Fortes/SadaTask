using Microsoft.EntityFrameworkCore;
using Sada.Api.Data;
using Sada.Api.HandlersApi;
using Sada.Core.Handlers;

namespace Sada.Api.Common
{
    public static class BuilderExtensions
    {
        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(n => n.FullName);
            });
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ITaskHandler, TaskHandler>();
        }

        public static void AddInMemory(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("TasksInMemory"));
        }
    }
}
