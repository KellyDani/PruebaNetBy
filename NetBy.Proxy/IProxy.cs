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
    public interface IProxy
    {
        string BaseAdress { get; set; }
        Task<PagedResponse<T>> GetPagedResponseAsync<T>(string uri, FilterQuery filter, PaginationQuery pagination);

        #region Productos
        Task<HttpResponseMessage> CreateProductosAsync(ProductosRequest createRequest);
        Task<HttpResponseMessage> UpdateProductosAsync(ProductosRequest updateRequest);
        Task<HttpResponseMessage> DeactivateProductosAsync(int entidadId);
        Task<HttpResponseMessage> ActivateProductosAsync(int entidadId);
        #endregion

        #region Categorias
        Task<HttpResponseMessage> CreateCategoriasAsync(CategoriasRequest createRequest);
        Task<HttpResponseMessage> UpdateCategoriasAsync(CategoriasRequest updateRequest);
        Task<HttpResponseMessage> DeactivateCategoriasAsync(int entidadId);
        Task<HttpResponseMessage> ActivateCategoriasAsync(int entidadId);
        #endregion

        #region Ventas
        Task<HttpResponseMessage> CreateVentasAsync(VentasRequest createRequest);
        Task<HttpResponseMessage> UpdateVentasAsync(VentasRequest updateRequest);
        Task<HttpResponseMessage> DeactivateVentasAsync(int entidadId);
     
        #endregion

        #region Compras
        Task<HttpResponseMessage> CreateComprasAsync(ComprasRequest createRequest);
        Task<HttpResponseMessage> UpdateComprasAsync(ComprasRequest updateRequest);
        Task<HttpResponseMessage> DeactivateComprasAsync(int entidadId);
    
        #endregion

    }
}
