using Sada.Api.Common;
using Sada.Api.Endpoints.TaskEndpoint;

namespace Sada.Api.Endpoints
{
    public  static class Endpoint
    {

        public static void MapEndpoint(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("/")
                .WithTags("Health check")
                .MapGet("/", () => new { message = "OK" });

            endpoints.MapGroup("v1/tarefas").WithTags("Tasks")
            .MapEndpoint<CreateTaskEndpoint>()
            .MapEndpoint<GetTaskByIdEndpoint>()
            .MapEndpoint<GetByParametersEndpoint>()
            .MapEndpoint<GetAllTaskEndpoint>()
            .MapEndpoint<DeleteTaskEndpoint>()
            .MapEndpoint<UpdateTaskEndpoint>();
            

        }

        private static IEndpointRouteBuilder MapEndpoint<TEndPoint>(this IEndpointRouteBuilder app) where TEndPoint : IEndpoint
        {
            TEndPoint.Map(app);
            return app;
        }
    }
}
