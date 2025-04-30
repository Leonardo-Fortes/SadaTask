using Sada.Api.Common;
using Sada.Core.Entities;
using Sada.Core.Handlers;
using Sada.Core.Requests.Task;
using Sada.Core.Responses;

namespace Sada.Api.Endpoints.TaskEndpoint
{
    public class GetTaskByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/by-id/{id}", HandlerAsync)
            .WithName("Task : GetById")
            .WithSummary("Busca uma tarefa")
            .WithDescription("Busca uma tarefa")
            .WithOrder(2)
            .Produces<Response<TaskSada>>();


        private static async Task<IResult>HandlerAsync(ITaskHandler handler, int id)
        {
            var request = new GetTaskByIdRequest
            {
                Id = id
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);

        }
    }
}
