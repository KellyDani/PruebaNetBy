using NetBy.Core.Modelos.Filters;
using NetBy.Core.Modelos;
using NetBy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace NetBy.Business.Servicios
{
    public class VentasService : IVentasService
    {
        private readonly DataContext _context;
        private readonly IProductosService _productosService;

        public VentasService(DataContext context, IProductosService productosService)
        {
            this._context = context;
            this._productosService = productosService;
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

        public List<Ventas> GetAll(FilterBase filter, PaginationFilter pagination)
        {
            List<Ventas> result = default;
            using (var transaction = CreateTrancationAsync())
            {
                var dynamicQuery = filter?.DynamicQuery ?? "";
                var queryParams = filter?.QueryParams ?? "";

                var query = queryParams != null ? queryParams.Split(";") : null;

                if (String.IsNullOrEmpty(dynamicQuery))
                {
                    result = _context.INV_Ventas.Include(x => x.Detalles)
                                       .ToList();
                }
                else
                {
                    //validate if query is only dinamic query
                    if (String.IsNullOrEmpty(queryParams))
                    {
                        result = _context.INV_Ventas.Include(x => x.Detalles)
                                        .Where(dynamicQuery)
                                        .ToList();
                    }
                    else
                    {
                        //or if query use query param
                        result = _context.INV_Ventas.Include(x => x.Detalles)
                                       .Where(dynamicQuery, query)
                                       .ToList();
                    }

                }

                var filtered = result.Where(d => d.Detalle.ToLower().Contains(filter.Code.ToLower()))
                                     .OrderBy(d => d.Detalle);

                result = filtered.Skip(pagination.PageSize * (pagination.PageNumber - 1)).Take(pagination.PageSize).ToList();

                transaction.Complete();
            }
            return result;
        }

        public async Task<ActionModel> Create(Ventas entidad)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ActionModel respuesta = new ActionModel();

                    entidad.Estado = "PENDIENTE";
                    entidad.FechaCreacion = DateTime.Now;
                    _context.INV_Ventas.Add(entidad);
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

        public async Task<ActionModel> Update(Ventas entidad)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ActionModel respuesta = new ActionModel();
                    var entidadToUpdate = _context.INV_Ventas.Include(x => x.Detalles).FirstOrDefault(x => x.Id == entidad.Id);
                    if (entidadToUpdate == null)
                    {
                        respuesta.Success = false;
                        respuesta.Mensaje = "No se encontró la venta a editar.";
                        return respuesta;
                    }

                    entidadToUpdate.Detalle = entidad.Detalle;
                    entidadToUpdate.Estado = entidad.Estado;
                    entidadToUpdate.FechaEdicion = DateTime.Now;
                    entidadToUpdate.Detalles = new List<VentasDt>();

                    if (entidad.Estado.Equals("PROCESADO"))
                    {
                        foreach (var item in entidad.Detalles)
                        {
                            var existeStock = _context.INV_Stock.FirstOrDefault(x => x.ProductoId == item.ProductoId);
                            if (existeStock == null)
                            {
                                respuesta.Success = false;
                                respuesta.Mensaje = "No existe suficiente stock.";
                                return respuesta;
                            }

                            if (existeStock.UnidadesStock < item.Cantidad)
                            {
                                respuesta.Success = false;
                                respuesta.Mensaje = "No existe suficiente stock.";
                                return respuesta;
                            }

                            Stock stock = new Stock
                            {
                                ProductoId = item.ProductoId,
                                UnidadesStock = existeStock.UnidadesStock - item.Cantidad
                            };

                            var responseStock = await _productosService.MovimientoStock(stock);
                            if (!responseStock.Success)
                            {
                                respuesta.Success = false;
                                respuesta.Mensaje = responseStock.Mensaje;
                                return respuesta;
                            }

                            Kardex kardex = new Kardex
                            {
                                ProductoId = item.ProductoId,
                                Cantidad = item.Cantidad,
                                Ingreso = false,
                                PrecioCosto = item.PrecioUnitario,
                                TransaccionId = entidadToUpdate.Id,
                            };

                            var responseKardex = await _productosService.MovimientoKardex(kardex);
                            if (!responseKardex.Success)
                            {
                                respuesta.Success = false;
                                respuesta.Mensaje = responseKardex.Mensaje;
                                return respuesta;
                            }

                            entidadToUpdate.Detalles.Add(item);
                        }
                    }

                    _context.INV_Ventas.Update(entidadToUpdate);
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

        public async Task<ActionModel> Desactivate(int ventaId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ActionModel respuesta = new ActionModel();
                    var entidadToUpdate = _context.INV_Ventas.Include(x=> x.Detalles).FirstOrDefault(x => x.Id == ventaId);
                    if (entidadToUpdate == null)
                    {
                        respuesta.Success = false;
                        respuesta.Mensaje = "No se encontró la venta a eliminar.";

                        await transaction.RollbackAsync();
                        return respuesta;
                    }

                    if (entidadToUpdate.Estado.Equals("PROCESADO"))
                    {
                        foreach (var item in entidadToUpdate.Detalles)
                        {

                            //reverso stock
                            var stock = _context.INV_Stock.FirstOrDefault(x => x.ProductoId == item.ProductoId);
                            if (stock == null)
                            {
                                respuesta.Success = false;
                                respuesta.Mensaje = "Ha ocurrido un error al intentar reversar el stock.";

                                await transaction.RollbackAsync();
                                return respuesta;
                            }

                            stock.UnidadesStock += item.Cantidad;
                            stock.UltimaEdicion = DateTime.Now;
                            _context.INV_Stock.Update(stock);

                            //reverso kardex
                            var kardex = _context.INV_Kardex.FirstOrDefault(x => x.TransaccionId == ventaId && x.ProductoId == item.ProductoId);
                            if (kardex == null) 
                            {
                                respuesta.Success = false;
                                respuesta.Mensaje = "Ha ocurrido un error al intentar reversar el kardex.";

                                await transaction.RollbackAsync();
                                return respuesta;
                            }

                            kardex.Anulado = true;
                            kardex.FechaEliminacion = DateTime.Now;
                            _context.INV_Kardex.Update(kardex);
                        }
                    }
                
                    entidadToUpdate.Estado = "ANULADO";
                    entidadToUpdate.Anulado = true;
                    entidadToUpdate.FechaEdicion = DateTime.Now;

                    _context.INV_Ventas.Update(entidadToUpdate);

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
    }
}