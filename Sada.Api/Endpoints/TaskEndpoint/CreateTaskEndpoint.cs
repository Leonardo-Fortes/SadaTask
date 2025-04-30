using Sada.Api.Common;
using Sada.Core.Entities;
using Sada.Core.Handlers;
using Sada.Core.Requests.Task;
using Sada.Core.Responses;

namespace Sada.Api.Endpoints.TaskEndpoint
{
    public class CreateTaskEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
       => app.MapPost("", HandlerAsync)
            .WithName("Task: Create")
            .WithSummary("Cria uma nova tarefa")
            .WithDescription("Cria uma nova tarefa")
            .WithOrder(1).Produces<Response<TaskSada?>>();


        private static async Task<IResult>HandlerAsync(CreateTaskRequest request, ITaskHandler handler)
        {
            var result = await handler.CreateAsync(request);

            return result.IsSuccess ? TypedResults.Created($"{result.Data?.Id}", result) : TypedResults.BadRequest(result);

        }
    }
}
