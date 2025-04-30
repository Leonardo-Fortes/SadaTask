using Sada.Api.Common;
using Sada.Core.Entities;
using Sada.Core.Handlers;
using Sada.Core.Requests.Task;
using Sada.Core.Responses;

namespace Sada.Api.Endpoints.TaskEndpoint
{
    public class DeleteTaskEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
       => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Task : Delete")
            .WithSummary("Deleta uma tarefa")
            .WithDescription("Deleta uma tarefa")
            .WithOrder(6)
            .Produces<Response<TaskSada?>>();

        private static async Task<IResult>HandlerAsync(ITaskHandler handler, int id)
        {
            var request = new DeleteTaskRequest
            {
                Id = id
            };

            var result = await handler.DeleteAsync(request);

            return result.IsSuccess ?
                TypedResults.Ok(result) :
                TypedResults.BadRequest(result);
        }
    }
}
