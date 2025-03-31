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
    public interface ICategoriasService
    {
        List<Categorias> GetAll(FilterBase filter, PaginationFilter pagination);
        Task<ActionModel> Create(Categorias entidad);
        Task<ActionModel> Update(Categorias entidad);
        Task<ActionModel> Desactivate(int categoriaId);
        Task<ActionModel> Activate(int categoriaId);
   

    }
}
