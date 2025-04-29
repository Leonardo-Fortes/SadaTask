using Sada.Core.Entities;
using Sada.Core.Requests.Task;
using Sada.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sada.Core.Handlers
{
   public interface  ITaskHandler
    {
        Task<Response<TaskSada?>>CreateAsync(CreateTaskRequest request);
        Task<Response<TaskSada?>>UpdateAsync(UpdateTaskRequest request);
        Task<Response<TaskSada?>> DeleteAsync(DeleteTaskRequest request);
        Task<Response<TaskSada?>> GetByIdAsync(GetTaskByIdRequest request);
        Task<PagedResponse<List<TaskSada?>>> GetAllTasksAsync(GetAllTaskRequest request);   
    }
}
