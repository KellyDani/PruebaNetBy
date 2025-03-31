using Microsoft.EntityFrameworkCore;
using NetBy.Core.Modelos;
using NetBy.Core.Modelos.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Business.Servicios
{
    public interface IProductosService
    {
        List<Productos> GetAll(FilterBase filter, PaginationFilter pagination);
        Task<ActionModel> Create(Productos entidad);
        Task<ActionModel> Update(Productos entidad);
        Task<ActionModel> Desactivate(int productoId);
        Task<ActionModel> Activate(int productoId);
        List<Kardex> GetInformeKardex(FilterBase filter, PaginationFilter pagination);
        List<Stock> GetInformeStock(FilterBase filter, PaginationFilter pagination);
        Task<ActionModel> MovimientoKardex(Kardex entidad);
        Task<ActionModel> MovimientoStock(Stock entidad);
    }
}
