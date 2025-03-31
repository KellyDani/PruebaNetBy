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
    public class VentasController : ApplicationControllerBase
    {
        private readonly IVentasService _service;

        public VentasController(IMapper mapper, IUriService uriService, IVentasService service) : base(mapper, uriService)
        {
            this._service = service;
        }

        [HttpGet(ApiRoutes.Ventas.GetAll)]
        public async Task<IActionResult> GetVentas([FromQuery] FilterQuery filterQuery, [FromQuery] PaginationQuery paginationQuery)
        {
            try
            {
                var filter = _mapper.Map<FilterBase>(filterQuery);
                var pagination = _mapper.Map<PaginationFilter>(paginationQuery);

                var ventas = _service.GetAll(filter, pagination);
                var ventasDTOs = _mapper.Map<List<VentasResponse>>(ventas);

                var pagedResponse = PaginationHelpers.CreatePaginatedResponse<VentasResponse>(_uriService,
                                                                                          pagination, ventasDTOs,
                                                                                          ApiRoutes.Ventas.GetAll);
                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ha ocurrido un error, revisar los detalles para mayor información: {ex.Message}");
            }

        }

        [HttpPost(ApiRoutes.Ventas.Create)]
        public async Task<IActionResult> Create(VentasRequest request)
        {
            ActionResponse respuesta = new ActionResponse();
            try
            {
                Ventas entidad = new Ventas();
                entidad.Detalle = request.Detalle;
                entidad.Estado = request.Estado;
                entidad.Detalles = new List<VentasDt>();

                foreach (var item in request.Detalles)
                {
                    entidad.Detalles.Add(new VentasDt
                    {
                        Cantidad = item.Cantidad,
                        PrecioTotal = item.PrecioTotal,
                        PrecioUnitario = item.PrecioUnitario,
                        ProductoId = item.ProductoId,
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

        [HttpPost(ApiRoutes.Ventas.Update)]
        public async Task<IActionResult> Update(VentasRequest request)
        {
            ActionResponse respuesta = new ActionResponse();

            try
            {
                Ventas entidad = new Ventas();
                entidad.Id = request.Id;
                entidad.Detalle = request.Detalle;
                entidad.Estado = request.Estado;
                entidad.Detalles = new List<VentasDt>();

                foreach (var item in request.Detalles)
                {
                    entidad.Detalles.Add(new VentasDt
                    {
                        Id = item.Id,
                        Cantidad = item.Cantidad,
                        PrecioTotal = item.PrecioTotal,
                        PrecioUnitario = item.PrecioUnitario,
                        ProductoId = item.ProductoId,
                        VentaId = item.VentaId
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

        [HttpPut(ApiRoutes.Ventas.Deactivate)]
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