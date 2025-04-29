using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sada.Core.Requests
{
    public abstract class PagedRequest
    {
        public int PageSize { get; private set; } = Configuration.DefaultPageSize;

        public int PageNumber { get; private set; } = Configuration.DefaultPageNumber;
    }
}
