using NetBy.Common.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Business.Servicios
{
    public interface IUriService
    {
        Uri GetUri(PaginationQuery pagination, string actionRoute);
    }
}
