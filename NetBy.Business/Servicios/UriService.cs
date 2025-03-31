using NetBy.Common.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NetBy.Business.Servicios
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }


        public Uri GetUri(PaginationQuery pagination, string actionRoute)
        {            
            var uriBuilder = new UriBuilder($"{_baseUri}{actionRoute}");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (pagination != null)
            {
                query["pageNumber"] = pagination.PageNumber.ToString();
                query["pageSize"] = pagination.PageSize.ToString();
            }

            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri;

        }


    }
}
