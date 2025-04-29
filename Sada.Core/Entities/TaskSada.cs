using Sada.Core.Enums;
using Sada.Core.Requests.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sada.Core.Entities
{
    public class TaskSada
    {
        public TaskSada(string title, string? description, DateTime? expirationDate, EStatus status)
        {
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            Status = status;
        }
     

        public int Id { get; private set; }

        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; } = string.Empty;

        public DateTime? ExpirationDate { get; private set; }

        public EStatus Status { get; private set; }


        public void AtualizaTask(string title, string? description, DateTime? expirationDate, EStatus status)
        {
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            Status = status;
        }
    }
}
