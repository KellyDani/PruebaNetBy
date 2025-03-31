using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetBy.Api.Helpers;
using NetBy.Business.Servicios;
using NetBy.Common.Request;
using NetBy.Common.Response;
using NetBy.Common;
using NetBy.Core.Modelos.Filters;
using NetBy.Core.Modelos;

namespace NetBy.Api.Controllers
{
    public class ComprasController : ApplicationControllerBase
    {
        private readonly IComprasService _service;

        public ComprasController(IMapper mapper, IUriService uriService, IComprasService service) : base(mapper, uriService)
        {
            this._service = service;
        }

        [HttpGet(ApiRoutes.Compras.GetAll)]
        public async Task<IActionResult> GetCompras([FromQuery] FilterQuery filterQuery, [FromQuery] PaginationQuery paginationQuery)
        {
            try
            {
                var filter = _mapper.Map<FilterBase>(filterQuery);
                var pagination = _mapper.Map<PaginationFilter>(paginationQuery);

                var compras = _service.GetAll(filter, pagination);
                var comprasDTOs = _mapper.Map<List<ComprasResponse>>(compras);

                var pagedResponse = PaginationHelpers.CreatePaginatedResponse<ComprasResponse>(_uriService,
                                                                                          pagination, comprasDTOs,
                                                                                          ApiRoutes.Compras.GetAll);
                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ha ocurrido un error, revisar los detalles para mayor información: {ex.Message}");
            }

        }

        [HttpPost(ApiRoutes.Compras.Create)]
        public async Task<IActionResult> Create(ComprasRequest request)
        {
            ActionResponse respuesta = new ActionResponse();
            try
            {
                Compras entidad = new Compras();
                entidad.Detalle = request.Detalle;
                entidad.Estado = request.Estado;
                entidad.Detalles = new List<ComprasDt>();

                foreach (var item in request.Detalles)
                {
                    entidad.Detalles.Add(new ComprasDt
                    {
                        Id = item.Id,
                        Cantidad = item.Cantidad,
                        ProductoId = item.ProductoId,
                        CostoUnitario = item.CostoUnitario,
                        CompraId = item.CompraId,
                        CostoTotal = item.CostoTotal,
                    });
                }

                var response = await _service.Create(entidad);

                respuesta.Success = response.Success;
                respuesta.Mensaje = response.Mensaje;

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ha ocurrido un error, revisar los detalles para mayor información: {ex.Message}");
            }

        }

        [HttpPost(ApiRoutes.Compras.Update)]
        public async Task<IActionResult> Update(ComprasRequest request)
        {
            ActionResponse respuesta = new ActionResponse();

            try
            {
                Compras entidad = new Compras();
                entidad.Id = request.Id;
                entidad.Detalle = request.Detalle;
                entidad.Estado = request.Estado;
                entidad.Detalles = new List<ComprasDt>();

                foreach (var item in request.Detalles)
                {
                    entidad.Detalles.Add(new ComprasDt
                    {
                        Id = item.Id,
                        Cantidad = item.Cantidad,                      
                        ProductoId = item.ProductoId,
                        CostoUnitario = item.CostoUnitario,
                        CompraId = item.CompraId,
                        CostoTotal = item.CostoTotal,                        
                    });
                }

                var response = await _service.Update(entidad);

                respuesta.Success = response.Success;
                respuesta.Mensaje = response.Mensaje;

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ha ocurrido un error, revisar los detalles para mayor información: {ex.Message}");
            }

        }

        [HttpPut(ApiRoutes.Compras.Deactivate)]
        public async Task<IActionResult> Desactivate([FromBody] int entidadId)
        {
            ActionResponse respuesta = new ActionResponse();
            try
            {
                var response = await _service.Desactivate(entidadId);

                respuesta.Success = response.Success;
                respuesta.Mensaje = response.Mensaje;

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ha ocurrido un error, revisar los detalles para mayor información: {ex.Message}");
            }

        }
    }
}