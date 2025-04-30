using Microsoft.AspNetCore.Mvc;
using Sada.Api.Common;
using Sada.Core;
using Sada.Core.Entities;
using Sada.Core.Enums;
using Sada.Core.Handlers;
using Sada.Core.Requests.Task;
using Sada.Core.Responses;


namespace Sada.Api.Endpoints.TaskEndpoint
{
    public class GetAllTaskEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Task : GetAll")
            .WithSummary("Busca todas as tarefas")
            .WithDescription("Busca todas as tarefas")
            .WithOrder(4)
            .Produces<Response<TaskSada>>();


        private static async Task<IResult> HandlerAsync(ITaskHandler handler)
        {
            var request = new GetAllTaskRequest
            {
              
            };

            var result = await handler.GetAllAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
