using Microsoft.EntityFrameworkCore;
using Sada.Api.Data;
using Sada.Core.Entities;
using Sada.Core.Enums;
using Sada.Core.Handlers;
using Sada.Core.Requests.Task;
using Sada.Core.Requests.TaskRequest;
using Sada.Core.Responses;
using System.Threading.Tasks;

namespace Sada.Api.HandlersApi
{
    public class TaskHandler(AppDbContext context, ILogger<TaskHandler> logger) : ITaskHandler
    {
       
        public async Task<Response<TaskSada?>> CreateAsync(CreateTaskRequest request)
        {
            try
            {   if(request.ExpirationDate < DateTime.Now)
                    return new Response<TaskSada?>(null, 400, "A data de expiração não pode ser menor que a data atual");

                if (!Enum.IsDefined(typeof(EStatus), request.Status))
                {
                    return new Response<TaskSada?>(null, 400, "Status inválido");
                }
                var task = new TaskSada(request.Title, request.Description, request.ExpirationDate, request.Status);
                await context.Tasks.AddAsync(task);
                await context.SaveChangesAsync();
                return new Response<TaskSada?>(task, 201, "Tarefa criada com sucesso");
            }
            catch(Exception ex) 
            {
                logger.LogWarning(ex,"Falha ao criar");
                return new Response<TaskSada?>(null, 500, "Falha ao criar");
            }
        }

        public async Task<Response<TaskSada?>> DeleteAsync(DeleteTaskRequest request)
        {
            try
            {
                var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (task is null)
                {
                    logger.LogWarning("Tarefa não encontrada");
                    return new Response<TaskSada?>(null, 404, "Tarefa não encontrada");
                }

                context.Tasks.Remove(task);
                await context.SaveChangesAsync();
                return new Response<TaskSada?>(task, message: "Tarefa removida com sucesso!");
            }
            catch(Exception ex)
            {
                logger.LogWarning(ex,"Falha ao remover");
                return new Response<TaskSada?>(null, 500, "Falha ao remover");
            }
        }

        public async Task<Response<List<TaskSada>>> GetAllAsync(GetAllTaskRequest request)
        {
            try
            {
                var tasks = await context.Tasks.AsNoTracking().ToListAsync();

                return new Response<List<TaskSada>>(tasks, 200, "Tarefas consultadas com sucesso");
            }
            catch(Exception ex)
            {
                logger.LogWarning(ex, "Falha ao listar todas");
                return new Response<List<TaskSada>>(null, 500, "Falha na consulta");
            }
        }


        public async Task<Response<TaskSada?>> GetByIdAsync(GetTaskByIdRequest request)
        {
            try
            {
                var task = await context.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
                return task is null ?
                    new Response<TaskSada?>(null, 404, "Tarefa não encontrada")
                    : new Response<TaskSada?>(task, message: "Tarefa não encontrada");
            }
            catch(Exception ex)
            {
                logger.LogWarning(ex, "Falha ao listar por Id");
                return new Response<TaskSada?>(null, 500, "Falha na consulta");
            }
        }

        public async Task<Response<List<TaskSada>>> GetByParametersAsync(GetTaskByParametersRequest request)
        {
            try
            {

                if (request.Status is null && request.ExpirationDate is null)
                    
                return new Response<List<TaskSada>>(null, 400, "É necessário informar ao menos um parâmetro");

                if (request.ExpirationDate is not null && request.ExpirationDate.Value.Date < DateTime.Now.Date)
                    return new Response<List<TaskSada>>(null, 400, "A data de expiração não pode ser menor que a data atual");

                var query = context.Tasks.AsNoTracking().AsQueryable();
         
                if (request.ExpirationDate is not null )               
                    query = query.Where(x => x.ExpirationDate.HasValue && x.ExpirationDate.Value.Date <= request.ExpirationDate.Value.Date);


                if (request.Status is not null)
                    query = query.Where(x => x.Status == request.Status);

                var tasks = await query.ToListAsync();

                return new Response<List<TaskSada>>(tasks, 200, "Tarefas consultadas com sucesso");
            }
            catch(Exception ex)
            {
                logger.LogWarning(ex, "Falha ao listar por periodo");
                return new Response<List<TaskSada>>(null, 500, "Falha na consulta");
            }
        }

  

        public async Task<Response<TaskSada?>> UpdateAsync(UpdateTaskRequest request)
        {
            try
            {
                if (request.ExpirationDate < DateTime.Now)
                    return new Response<TaskSada?>(null, 400, "A data de expiração não pode ser menor que a data atual");

                if (!Enum.IsDefined(typeof(EStatus), request.Status))
                {
                    return new Response<TaskSada?>(null, 400, "Status inválido");
                }
                var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (task is null)
                    return new Response<TaskSada?>(null, 404, "Tarefa não encontrada");

                task.AtualizaTask(request.Title, request.Description, request.ExpirationDate, request.Status);
                await context.SaveChangesAsync();
                return new Response<TaskSada?>(task, 201, "Tarefa atualizada com sucesso");
            }
            catch
            {
                return new Response<TaskSada?>(null, 500, "Falha ao atualizar");
            }
        }

    }
}
