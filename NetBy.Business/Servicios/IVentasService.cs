using NetBy.Core.Modelos.Filters;
using NetBy.Core.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Business.Servicios
{
    public interface IVentasService
    {
        List<Ventas> GetAll(FilterBase filter, PaginationFilter pagination);
        Task<ActionModel> Create(Ventas entidad);
        Task<ActionModel> Update(Ventas entidad);
        Task<ActionModel> Desactivate(int ventaId);
    }
}
