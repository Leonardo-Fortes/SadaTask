using Sada.Core.Entities;
using Sada.Core.Requests.Task;
using Sada.Core.Requests.TaskRequest;
using Sada.Core.Responses;

namespace Sada.Core.Handlers
{
   public interface  ITaskHandler
    {
        Task<Response<TaskSada?>>CreateAsync(CreateTaskRequest request);
        Task<Response<TaskSada?>>UpdateAsync(UpdateTaskRequest request);
        Task<Response<TaskSada?>> DeleteAsync(DeleteTaskRequest request);
        Task<Response<TaskSada?>> GetByIdAsync(GetTaskByIdRequest request);
        Task<Response<List<TaskSada>>> GetAllAsync(GetAllTaskRequest request);
        Task<Response<List<TaskSada>>> GetByParametersAsync(GetTaskByParametersRequest request);
    }
}
