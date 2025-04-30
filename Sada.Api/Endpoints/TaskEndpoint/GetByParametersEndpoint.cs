using Microsoft.AspNetCore.Mvc;
using Sada.Api.Common;
using Sada.Core.Entities;
using Sada.Core.Enums;
using Sada.Core.Handlers;

using Sada.Core.Requests.TaskRequest;
using Sada.Core.Responses;

namespace Sada.Api.Endpoints.TaskEndpoint
{
    public class GetByParametersEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
      => app.MapGet("/task-periodo", HandlerAsync)
          .WithName("Task : GetByPeriod")
          .WithSummary("Busca tarefas por parâmetros")
          .WithDescription("Busca tarefas por parâmetros")
          .WithOrder(3)
          .Produces<Response<TaskSada>>();

        private static async Task<IResult> HandlerAsync(ITaskHandler handler, [FromQuery] EStatus? status, [FromQuery] DateTime? expirationDate)
        {
            var request = new GetTaskByParametersRequest
            {
                Status = status,
                ExpirationDate = expirationDate
            };
            var result = await handler.GetByParametersAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);

        }

    }
}

