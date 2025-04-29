using Sada.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sada.Core.Requests.Task
{
    public class UpdateTaskRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Titulo Inválido")]
        [MaxLength(180, ErrorMessage = "O titulo deve ter até 180 caracteres")]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public DateTime? ExpirationDate { get; set; }

        public EStatus Status { get; set; }
    }
}
