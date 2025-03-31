using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetBy.Business.Servicios;
using NetBy.Common.Request;
using NetBy.Common.Response;
using NetBy.Common;
using NetBy.Core.Modelos;
using NetBy.Api.Helpers;
using NetBy.Core.Modelos.Filters;

namespace NetBy.Api.Controllers
{
    public class CategoriasController : ApplicationControllerBase
    {
        private readonly ICategoriasService _service;

        public CategoriasController(IMapper mapper, IUriService uriService, ICategoriasService service) : base(mapper, uriService)
        {
            this._service = service;
        }

        [HttpGet(ApiRoutes.Categorias.GetAll)]
        public async Task<IActionResult> GetCategorias([FromQuery] FilterQuery filterQuery, [FromQuery] PaginationQuery paginationQuery)
        {
            try
            {
                var filter = _mapper.Map<FilterBase>(filterQuery);
                var pagination = _mapper.Map<PaginationFilter>(paginationQuery);

                var categorias = _service.GetAll(filter, pagination);
                var categoriasDTOs = _mapper.Map<List<CategoriasResponse>>(categorias);

                var pagedResponse = PaginationHelpers.CreatePaginatedResponse<CategoriasResponse>(_uriService,
                                                                                          pagination, categoriasDTOs,
                                                                                          ApiRoutes.Categorias.GetAll);
                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ha ocurrido un error, revisar los detalles para mayor información: {ex.Message}");
            }

        }

        [HttpPost(ApiRoutes.Categorias.Create)]
        public async Task<IActionResult> Create(CategoriasRequest request)
        {
            ActionResponse respuesta = new ActionResponse();
            try
            {
                Categorias entidad = new Categorias();
                entidad.Nombre = request.Nombre;
                entidad.Descripcion = request.Descripcion;
               
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

        [HttpPost(ApiRoutes.Categorias.Update)]
        public async Task<IActionResult> Update(CategoriasRequest request)
        {
            ActionResponse respuesta = new ActionResponse();
            try
            {
                Categorias entidad = new Categorias();
                entidad.Id = request.Id;
                entidad.Nombre = request.Nombre;
                entidad.Descripcion = request.Descripcion;

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

        [HttpPut(ApiRoutes.Categorias.Deactivate)]
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

        [HttpPut(ApiRoutes.Categorias.Activate)]
        public async Task<IActionResult> Activate([FromBody] int entidadId)
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

    }
}
