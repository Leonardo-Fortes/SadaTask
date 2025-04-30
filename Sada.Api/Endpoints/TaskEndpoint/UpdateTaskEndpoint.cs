using Sada.Api.Common;
using Sada.Core.Entities;
using Sada.Core.Handlers;
using Sada.Core.Requests.Task;
using Sada.Core.Responses;

namespace Sada.Api.Endpoints.TaskEndpoint
{
    public class UpdateTaskEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
      => app.MapPut("/{id}", HandlerAsync)
            .WithName("Task : Update")
            .WithSummary("Atualiza uma tarefa")
            .WithDescription("Atualiza uma tarefa")
            .WithOrder(5)
            .Produces<Response<TaskSada>>();

        private static async Task<IResult> HandlerAsync(UpdateTaskRequest request, ITaskHandler handler, int id)
        {
            request.Id = id;
            var result = await handler.UpdateAsync(request);

            return result.IsSuccess ?
                   TypedResults.Ok(result) :
                   TypedResults.BadRequest(result);
        }
    }
}
