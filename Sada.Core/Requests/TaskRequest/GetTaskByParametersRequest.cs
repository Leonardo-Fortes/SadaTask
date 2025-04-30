using Sada.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sada.Core.Requests.TaskRequest
{
    public class GetTaskByParametersRequest
    {
      
        public DateTime? ExpirationDate { get; set; }
        public EStatus? Status { get; set; }
    }
}
