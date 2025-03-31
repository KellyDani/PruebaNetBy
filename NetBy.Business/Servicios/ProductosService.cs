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
    public class ProductosService : IProductosService
    {
        private readonly DataContext _context;

        public ProductosService(DataContext context)
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

        public List<Productos> GetAll(FilterBase filter, PaginationFilter pagination)
        {
            List<Productos> result = default;
            using (var transaction = CreateTrancationAsync())
            {
                var dynamicQuery = filter?.DynamicQuery ?? "";
                var queryParams = filter?.QueryParams ?? "";

                var query = queryParams != null ? queryParams.Split(";") : null;

                if (String.IsNullOrEmpty(dynamicQuery))
                {
                    result = _context.INV_Productos.Include(x => x.Categoria)
                                       .ToList();
                }
                else
                {
                    //validate if query is only dinamic query
                    if (String.IsNullOrEmpty(queryParams))
                    {
                        result = _context.INV_Productos.Include(x => x.Categoria)
                                        .Where(dynamicQuery)
                                        .ToList();
                    }
                    else
                    {
                        //or if query use query param
                        result = _context.INV_Productos.Include(x => x.Categoria)
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

        public async Task<ActionModel> Create(Productos entidad)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ActionModel respuesta = new ActionModel();
                    if (entidad.PrecioVenta == 0)
                    {
                        respuesta.Success = false;
                        respuesta.Mensaje = "El precio de venta del producto no puede ser cero.";
                        return respuesta;
                    }

                    entidad.FechaCreacion = DateTime.Now;
                    _context.INV_Productos.Add(entidad);
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

        public async Task<ActionModel> MovimientoKardex(Kardex entidad)
        {

            ActionModel respuesta = new ActionModel();
            try
            {
                
                _context.INV_Kardex.Add(entidad);
                _context.SaveChanges();

                respuesta.Success = true;
                respuesta.Mensaje = "Registro guardado con éxito.";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Success = true;
                respuesta.Mensaje = "Ha ocurrido un error al registrar el movimiento del kardex.";
                return respuesta;
            }
        }

        public async Task<ActionModel> MovimientoStock(Stock entidad)
        {
            ActionModel respuesta = new ActionModel();
            try
            {

                if (entidad.ProductoId == 0)
                {
                    respuesta.Success = false;
                    respuesta.Mensaje = "Debe indicar el producto.";

                    return respuesta;
                }

                var existeStock = _context.INV_Stock.FirstOrDefault(x => x.ProductoId == entidad.ProductoId);
                if (existeStock == null)//si no existe es primera vez que ingresa
                {
                    _context.INV_Stock.Add(entidad);
                    _context.SaveChanges();
                    respuesta.Success = true;
                    respuesta.Mensaje = "Registro guardado con éxito.";
                }
                else //si ya existe es un ingreso/egreso
                {
                    if (entidad.UnidadesStock <= 0)
                    {
                        respuesta.Success = false;
                        respuesta.Mensaje = "El stock no puede ser negativo.";
                        return respuesta;
                    }

                    existeStock.UnidadesStock = entidad.UnidadesStock;
                    existeStock.UltimaEdicion = DateTime.Now;
                    _context.INV_Stock.Update(existeStock);
                    _context.SaveChanges();

                    respuesta.Success = true;
                    respuesta.Mensaje = "Registro actualizado con éxito.";
                }
               
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Success = true;
                respuesta.Mensaje = "Ha ocurrido un error al registrar el movimiento del stock.";
                return respuesta;
            }

        }

        public async Task<ActionModel> Update(Productos entidad)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ActionModel respuesta = new ActionModel();
                    var entidadToUpdate = _context.INV_Productos.FirstOrDefault(x => x.Id == entidad.Id);
                    if (entidadToUpdate == null)
                    {
                        respuesta.Success = false;
                        respuesta.Mensaje = "No se encontró el producto a editar.";
                    }

                    entidadToUpdate.Nombre = entidad.Nombre;
                    entidadToUpdate.Descripcion = entidad.Descripcion;
                    entidadToUpdate.CategoriaId = entidad.CategoriaId;
                    entidadToUpdate.RutaImagen = entidad.RutaImagen;
                    entidadToUpdate.PrecioVenta = entidad.PrecioVenta;
                    entidadToUpdate.FechaUltimaEdicion = DateTime.Now;

                    _context.INV_Productos.Update(entidadToUpdate);
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

        public async Task<ActionModel> Desactivate(int productoId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ActionModel respuesta = new ActionModel();
                    var entidadToUpdate = _context.INV_Productos.FirstOrDefault(x => x.Id == productoId);
                    if (entidadToUpdate == null)
                    {
                        respuesta.Success = false;
                        respuesta.Mensaje = "No se encontró el producto a eliminar.";
                    }

                    entidadToUpdate.Anulado = true;
                    entidadToUpdate.FechaUltimaEdicion = DateTime.Now;

                    _context.INV_Productos.Update(entidadToUpdate);
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

        public async Task<ActionModel> Activate(int productoId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ActionModel respuesta = new ActionModel();
                    var entidadToUpdate = _context.INV_Productos.FirstOrDefault(x => x.Id == productoId);
                    if (entidadToUpdate == null)
                    {
                        respuesta.Success = false;
                        respuesta.Mensaje = "No se encontró el producto a activar.";
                    }

                    entidadToUpdate.Anulado = false;
                    entidadToUpdate.FechaUltimaEdicion = DateTime.Now;

                    _context.INV_Productos.Update(entidadToUpdate);
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


        public List<Stock> GetInformeStock(FilterBase filter, PaginationFilter pagination)
        {
            List<Stock> result = default;
            using (var transaction = CreateTrancationAsync())
            {
                var dynamicQuery = filter?.DynamicQuery ?? "";
                var queryParams = filter?.QueryParams ?? "";

                var query = queryParams != null ? queryParams.Split(";") : null;

                if (String.IsNullOrEmpty(dynamicQuery))
                {
                    result = _context.INV_Stock.Include(x => x.Producto).ThenInclude(x => x.Categoria)
                                       .ToList();
                }
                else
                {
                    //validate if query is only dinamic query
                    if (String.IsNullOrEmpty(queryParams))
                    {
                        result = _context.INV_Stock.Include(x => x.Producto).ThenInclude(x => x.Categoria)
                                        .Where(dynamicQuery)
                                        .ToList();
                    }
                    else
                    {
                        //or if query use query param
                        result = _context.INV_Stock.Include(x => x.Producto).ThenInclude(x=> x.Categoria)
                                       .Where(dynamicQuery, query)
                                       .ToList();
                    }

                }

                var filtered = result.Where(d => d.Producto.Nombre.ToLower().Contains(filter.Code.ToLower())
                                              || d.Producto.Descripcion.ToLower().Contains(filter.Code.ToLower()))
                                     .OrderBy(d => d.Producto.Nombre);

                result = filtered.Skip(pagination.PageSize * (pagination.PageNumber - 1)).Take(pagination.PageSize).ToList();

                transaction.Complete();
            }
            return result;
        }


        public List<Kardex> GetInformeKardex(FilterBase filter, PaginationFilter pagination)
        {
            List<Kardex> result = default;
            using (var transaction = CreateTrancationAsync())
            {
                var dynamicQuery = filter?.DynamicQuery ?? "";
                var queryParams = filter?.QueryParams ?? "";

                var query = queryParams != null ? queryParams.Split(";") : null;

                if (String.IsNullOrEmpty(dynamicQuery))
                {
                    result = _context.INV_Kardex.Include(x => x.Producto).ThenInclude(x => x.Categoria)
                                       .ToList();
                }
                else
                {
                    //validate if query is only dinamic query
                    if (String.IsNullOrEmpty(queryParams))
                    {
                        result = _context.INV_Kardex.Include(x => x.Producto).ThenInclude(x => x.Categoria)
                                        .Where(dynamicQuery)
                                        .ToList();
                    }
                    else
                    {
                        //or if query use query param
                        result = _context.INV_Kardex.Include(x => x.Producto).ThenInclude(x => x.Categoria)
                                       .Where(dynamicQuery, query)
                                       .ToList();
                    }

                }

                var filtered = result.Where(d => d.Producto.Nombre.ToLower().Contains(filter.Code.ToLower())
                                              || d.Producto.Descripcion.ToLower().Contains(filter.Code.ToLower()))
                                     .OrderBy(d => d.Producto.Nombre);

                result = filtered.Skip(pagination.PageSize * (pagination.PageNumber - 1)).Take(pagination.PageSize).ToList();

                transaction.Complete();
            }
            return result;
        }
       
    }
}
