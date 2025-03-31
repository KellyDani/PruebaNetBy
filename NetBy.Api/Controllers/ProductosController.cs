using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetBy.Api.Helpers;
using NetBy.Business.Servicios;
using NetBy.Common;
using NetBy.Common.Request;
using NetBy.Common.Response;
using NetBy.Core.Modelos;
using NetBy.Core.Modelos.Filters;
using NetBy.Data;

namespace NetBy.Api.Controllers
{
    public class ProductosController : ApplicationControllerBase
    {
        private readonly IProductosService _service;

        public ProductosController(IMapper mapper, IUriService uriService, IProductosService service) : base(mapper, uriService)
        {
            this._service = service;
        }

        [HttpGet(ApiRoutes.Productos.GetAll)]
        public async Task<IActionResult> GetProductos([FromQuery] FilterQuery filterQuery, [FromQuery] PaginationQuery paginationQuery)
        {
            try
            {
                var filter = _mapper.Map<FilterBase>(filterQuery);
                var pagination = _mapper.Map<PaginationFilter>(paginationQuery);

                var productos = _service.GetAll(filter, pagination);
                var productosDTOs = _mapper.Map<List<ProductosResponse>>(productos);

                var pagedResponse = PaginationHelpers.CreatePaginatedResponse<ProductosResponse>(_uriService,
                                                                                          pagination, productosDTOs,
                                                                                          ApiRoutes.Productos.GetAll);
                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ha ocurrido un error, revisar los detalles para mayor información: {ex.Message}");               
            }
          
        }

        [HttpPost(ApiRoutes.Productos.Create)]
        public async Task<ActionResult<ActionResponse>> CreateProducto(ProductosRequest request)
        {
            ActionResponse respuesta = new ActionResponse();
            try
            {
                Productos entidad = new Productos();
                entidad.Nombre = request.Nombre;
                entidad.Descripcion = request.Descripcion;
                entidad.PrecioVenta = request.PrecioVenta;
                entidad.CategoriaId = request.CategoriaId;
                entidad.RutaImagen = request.RutaImagen;

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

        [HttpPost(ApiRoutes.Productos.Update)]
        public async Task<ActionResult<ActionResponse>> UpdateProducto(ProductosRequest request)
        {
            ActionResponse respuesta = new ActionResponse();
            try
            {
                Productos entidad = new Productos();
                entidad.Id = request.Id;
                entidad.Nombre = request.Nombre;
                entidad.Descripcion = request.Descripcion;
                entidad.PrecioVenta = request.PrecioVenta;
                entidad.CategoriaId = request.CategoriaId;
                entidad.RutaImagen = request.RutaImagen;

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

        [HttpPut(ApiRoutes.Productos.Deactivate)]
        public async Task<ActionResult<ActionResponse>> DesactivateProducto([FromBody] int entidadId)
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

        [HttpPut(ApiRoutes.Productos.Activate)]
        public async Task<ActionResult<ActionResponse>> ActivateProducto([FromBody] int entidadId)
        {
            ActionResponse respuesta = new ActionResponse();
            try
            {
                var response = await _service.Activate(entidadId);

                respuesta.Success = response.Success;
                respuesta.Mensaje = response.Mensaje;

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ha ocurrido un error, revisar los detalles para mayor información: {ex.Message}");
            }

        }

        [HttpGet(ApiRoutes.Productos.GetInformeStock)]
        public async Task<IActionResult> GetInformeStock([FromQuery] FilterQuery filterQuery, [FromQuery] PaginationQuery paginationQuery)
        {
            try
            {
                var filter = _mapper.Map<FilterBase>(filterQuery);
                var pagination = _mapper.Map<PaginationFilter>(paginationQuery);

                var stock = _service.GetInformeStock(filter, pagination);
                var stockDTOs = _mapper.Map<List<StockResponse>>(stock);

                var pagedResponse = PaginationHelpers.CreatePaginatedResponse<StockResponse>(_uriService,
                                                                                          pagination, stockDTOs,
                                                                                          ApiRoutes.Productos.GetInformeStock);
                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ha ocurrido un error, revisar los detalles para mayor información: {ex.Message}");
            }

        }

        [HttpGet(ApiRoutes.Productos.GetInformeKardex)]
        public async Task<IActionResult> GetInformeKardex([FromQuery] FilterQuery filterQuery, [FromQuery] PaginationQuery paginationQuery)
        {
            try
            {
                var filter = _mapper.Map<FilterBase>(filterQuery);
                var pagination = _mapper.Map<PaginationFilter>(paginationQuery);

                var kardex = _service.GetInformeKardex(filter, pagination);
                var kardexDTOs = _mapper.Map<List<KardexResponse>>(kardex);

                var pagedResponse = PaginationHelpers.CreatePaginatedResponse<KardexResponse>(_uriService,
                                                                                          pagination, kardexDTOs,
                                                                                          ApiRoutes.Productos.GetInformeKardex);
                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ha ocurrido un error, revisar los detalles para mayor información: {ex.Message}");
            }

        }
    }
}
