using NetBy.Core.Modelos;
using NetBy.Core.Modelos.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Business.Servicios
{
    public interface IComprasService
    {
        List<Compras> GetAll(FilterBase filter, PaginationFilter pagination);
        Task<ActionModel> Create(Compras entidad);
        Task<ActionModel> Update(Compras entidad);
        Task<ActionModel> Desactivate(int compraId);
    }
}
