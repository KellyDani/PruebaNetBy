using NetBy.Common;
using NetBy.Common.Request;
using NetBy.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Proxy
{
    public class Proxy : BaseProxy, IProxy
    {
        public Proxy(HttpClient client) : base(client)
        {
        }

        public async Task<PagedResponse<T>> GetPagedResponseAsync<T>(string uri, FilterQuery filter, PaginationQuery pagination)
        {
            var response = await HttpGetAsync<PagedResponse<T>>(uri, filter, pagination);
            return response;
        }

        #region Productos
        public async Task<HttpResponseMessage> CreateProductosAsync(ProductosRequest createRequest)
        {
            var response = await HttpPostAsJsonAsync(ApiRoutes.Productos.Create, createRequest);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateProductosAsync(ProductosRequest updateRequest)
        {
            var response = await HttpPostAsJsonAsync(ApiRoutes.Productos.Update, updateRequest);
            return response;
        }      

        public async Task<HttpResponseMessage> DeactivateProductosAsync(int entidadId)
        {
            var response = await HttpPutAsJsonAsync(ApiRoutes.Productos.Deactivate, entidadId);
            return response;
        }

        public async Task<HttpResponseMessage> ActivateProductosAsync(int entidadId)
        {
            var response = await HttpPutAsJsonAsync(ApiRoutes.Productos.Activate, entidadId);
            return response;
        }
        #endregion

        #region Categorias
        public async Task<HttpResponseMessage> CreateCategoriasAsync(CategoriasRequest createRequest)
        {
            var response = await HttpPostAsJsonAsync(ApiRoutes.Categorias.Create, createRequest);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateCategoriasAsync(CategoriasRequest updateRequest)
        {
            var response = await HttpPostAsJsonAsync(ApiRoutes.Categorias.Update, updateRequest);
            return response;
        }    

        public async Task<HttpResponseMessage> DeactivateCategoriasAsync(int entidadId)
        {
            var response = await HttpPutAsJsonAsync(ApiRoutes.Categorias.Deactivate, entidadId);
            return response;
        }

        public async Task<HttpResponseMessage> ActivateCategoriasAsync(int entidadId)
        {
            var response = await HttpPutAsJsonAsync(ApiRoutes.Categorias.Activate, entidadId);
            return response;
        }
        #endregion

        #region Ventas
        public async Task<HttpResponseMessage> CreateVentasAsync(VentasRequest createRequest)
        {
            var response = await HttpPostAsJsonAsync(ApiRoutes.Ventas.Create, createRequest);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateVentasAsync(VentasRequest updateRequest)
        {
            var response = await HttpPostAsJsonAsync(ApiRoutes.Ventas.Update, updateRequest);
            return response;
        }

        public async Task<HttpResponseMessage> DeactivateVentasAsync(int entidadId)
        {
            var response = await HttpPutAsJsonAsync(ApiRoutes.Ventas.Deactivate, entidadId);
            return response;
        }
        #endregion

        #region Compras
        public async Task<HttpResponseMessage> CreateComprasAsync(ComprasRequest createRequest)
        {
            var response = await HttpPostAsJsonAsync(ApiRoutes.Compras.Create, createRequest);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateComprasAsync(ComprasRequest updateRequest)
        {
            var response = await HttpPostAsJsonAsync(ApiRoutes.Compras.Update, updateRequest);
            return response;
        }

        public async Task<HttpResponseMessage> DeactivateComprasAsync(int entidadId)
        {
            var response = await HttpPutAsJsonAsync(ApiRoutes.Compras.Deactivate, entidadId);
            return response;
        }
        #endregion

    }
}
