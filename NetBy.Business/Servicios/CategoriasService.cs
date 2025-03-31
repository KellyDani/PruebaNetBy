using Microsoft.EntityFrameworkCore;
using NetBy.Core.Modelos;
using NetBy.Core.Modelos.Filters;
using NetBy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NetBy.Business.Servicios
{
    public class CategoriasService: ICategoriasService
    {
        private readonly DataContext _context;

        public CategoriasService(DataContext context)
        {
            this._context = context;
        }

        public static TransactionScope CreateTrancationAsync()
        {
            return new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled);
        }

        public List<Categorias> GetAll(FilterBase filter, PaginationFilter pagination)
        {
            List<Categorias> result = default;
            using (var transaction = CreateTrancationAsync())
            {
                var dynamicQuery = filter?.DynamicQuery ?? "";
                var queryParams = filter?.QueryParams ?? "";

                var query = queryParams != null ? queryParams.Split(";") : null;

                if (String.IsNullOrEmpty(dynamicQuery))
                {
                    result = _context.INV_Categorias
                                       .ToList();
                }
                else
                {
                    //validate if query is only dinamic query
                    if (String.IsNullOrEmpty(queryParams))
                    {
                        result = _context.INV_Categorias
                                        .Where(dynamicQuery)
                                        .ToList();
                    }
                    else
                    {
                        //or if query use query param
                        result = _context.INV_Categorias
                                       .Where(dynamicQuery, query)
                                       .ToList();
                    }

                }

                var filtered = result.Where(d => d.Nombre.ToLower().Contains(filter.Code.ToLower())
                                              || d.Descripcion.ToLower().Contains(filter.Code.ToLower()))
                                     .OrderBy(d => d.Nombre);

                result = filtered.Skip(pagination.PageSize * (pagination.PageNumber - 1)).Take(pagination.PageSize).ToList();

                transaction.Complete();
            }
            return result;
        }


        public async Task<ActionModel> Create(Categorias entidad)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ActionModel respuesta = new ActionModel();
                   
                    entidad.FechaCreacion = DateTime.Now;
                    _context.INV_Categorias.Add(entidad);
                    _context.SaveChanges();

                    await transaction.CommitAsync();
                    respuesta.Success = true;
                    respuesta.Mensaje = "Registro guardado con éxito.";

                    return respuesta;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<ActionModel> Update(Categorias entidad)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ActionModel respuesta = new ActionModel();
                    var entidadToUpdate = _context.INV_Categorias.FirstOrDefault(x => x.Id == entidad.Id);
                    if (entidadToUpdate == null)
                    {
                        respuesta.Success = false;
                        respuesta.Mensaje = "No se encontró la categoria a editar.";
                    }

                    entidadToUpdate.Nombre = entidad.Nombre;
                    entidadToUpdate.Descripcion = entidad.Descripcion;                  
                    entidadToUpdate.FechaUltimaEdicion = DateTime.Now;

                    _context.INV_Categorias.Update(entidadToUpdate);
                    _context.SaveChanges();

                    await transaction.CommitAsync();
                    respuesta.Success = true;
                    respuesta.Mensaje = "Registro editado con éxito.";

                    return respuesta;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<ActionModel> Desactivate(int categoriaId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ActionModel respuesta = new ActionModel();
                    var entidadToUpdate = _context.INV_Categorias.FirstOrDefault(x => x.Id == categoriaId);
                    if (entidadToUpdate == null)
                    {
                        respuesta.Success = false;
                        respuesta.Mensaje = "No se encontró la categoria a eliminar.";
                    }

                    entidadToUpdate.Anulado = true;
                    entidadToUpdate.FechaUltimaEdicion = DateTime.Now;

                    _context.INV_Categorias.Update(entidadToUpdate);
                    _context.SaveChanges();

                    await transaction.CommitAsync();
                    respuesta.Success = true;
                    respuesta.Mensaje = "Registro eliminado con éxito.";

                    return respuesta;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<ActionModel> Activate(int categoriaId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ActionModel respuesta = new ActionModel();
                    var entidadToUpdate = _context.INV_Categorias.FirstOrDefault(x => x.Id == categoriaId);
                    if (entidadToUpdate == null)
                    {
                        respuesta.Success = false;
                        respuesta.Mensaje = "No se encontró la categoria a activar.";
                    }

                    entidadToUpdate.Anulado = false;
                    entidadToUpdate.FechaUltimaEdicion = DateTime.Now;

                    _context.INV_Categorias.Update(entidadToUpdate);
                    _context.SaveChanges();

                    await transaction.CommitAsync();
                    respuesta.Success = true;
                    respuesta.Mensaje = "Registro activado con éxito.";

                    return respuesta;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

    }
}
