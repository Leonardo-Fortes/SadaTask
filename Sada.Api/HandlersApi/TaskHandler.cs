using Microsoft.EntityFrameworkCore;
using Sada.Api.Data;
using Sada.Core.Entities;
using Sada.Core.Handlers;
using Sada.Core.Requests.Task;
using Sada.Core.Responses;
using System.Threading.Tasks;

namespace Sada.Api.HandlersApi
{
    public class TaskHandler(AppDbContext context) : ITaskHandler
    {
        public async Task<Response<TaskSada?>> CreateAsync(CreateTaskRequest request)
        {
            try
            {
                var task = new TaskSada(request.Title, request.Description, request.ExpirationDate, request.Status);
                await context.Tasks.AddAsync(task);
                await context.SaveChangesAsync();
                return new Response<TaskSada?>(task, 201, "Task criada com sucesso");
            }
            catch
            {
                return new Response<TaskSada?>(null, 500, "Não foi possivel criar a task");
            }
        }

        public async Task<Response<TaskSada?>> DeleteAsync(DeleteTaskRequest request)
        {
            try
            {
                var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (task is null)
                    return new Response<TaskSada?>(null, 404, "Task não encontrada");

                context.Tasks.Remove(task);
                await context.SaveChangesAsync();
                return new Response<TaskSada?>(task, message: "Task removida com sucesso!");
            }
            catch
            {
                return new Response<TaskSada?>(null, 404, "Não foi possivel remover a Task");
            }
        }

        public async Task<PagedResponse<List<TaskSada?>>> GetAllTasksAsync(GetAllTaskRequest request)
        {
            try
            {
                var query = context.Tasks.AsNoTracking().OrderBy(x => x.Title);
                var task = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                var count = await query.CountAsync();
                
                return new PagedResponse<List<TaskSada?>>(task, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<TaskSada?>>(null, 500, "Não foi possivel consultar as categorias");
            }
        }

        public async Task<Response<TaskSada?>> GetByIdAsync(GetTaskByIdRequest request)
        {
            try
            {
                var task = await context.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
                return task is null ?
                    new Response<TaskSada?>(null, 404, "Task não encontrada")
                    : new Response<TaskSada?>(task, message: "Task não encontrada");
            }
            catch
            {
                return new Response<TaskSada?>(null, 404, "Não foi possivel encontrar a Task");
            }
        }

        public async Task<Response<TaskSada?>> UpdateAsync(UpdateTaskRequest request)
        {
            try
            {
                var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == request.Id);
                
            }
            catch
            {

            }
        }


    }
}
