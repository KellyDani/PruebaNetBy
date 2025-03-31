using NetBy.Business.Servicios;
using NetBy.Common.Request;
using NetBy.Common.Response;
using NetBy.Core.Modelos.Filters;

namespace NetBy.Api.Helpers
{
    public class PaginationHelpers
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService,
                                       PaginationFilter pagination,
                                       List<T> response,
                                       string actionRoute)
        {
            var nextPage = pagination.PageNumber >= 1
               ? uriService.GetUri(new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize), actionRoute).ToString()
               : null;

            var previousPage = pagination.PageNumber - 1 >= 1
                ? uriService.GetUri(new PaginationQuery(pagination.PageNumber - 1, pagination.PageSize), actionRoute).ToString()
                : null;

            return new PagedResponse<T>
            {
                Data = response,
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?)null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
                NextPage = response.Any() ? nextPage : null,
                PreviousPage = previousPage
            };
        }

       

    }
}
