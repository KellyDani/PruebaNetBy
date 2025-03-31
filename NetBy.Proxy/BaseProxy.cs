using NetBy.Common.Request;
using NetBy.Common.Response;
using System.Net.Http.Json;
using System.Text.Json;

namespace NetBy.Proxy
{
    public abstract class BaseProxy
    {
        private readonly HttpClient _client;

        private string _baseAdress;
        public string BaseAdress
        {
            get { return _baseAdress; }
            set { _baseAdress = value; }
        }

        public BaseProxy(HttpClient client)
        {
            this._client = client;
        }

        protected async Task<T> HttpGetAsync<T>(string uri, FilterQuery filter, PaginationQuery pagination)
        {
            try
            {
                // Crear un diccionario para los parámetros de la consulta
                var query = new Dictionary<string, string>();

                // Solo agregar parámetros si tienen valor
                if (!string.IsNullOrEmpty(filter.Id))
                    query["Id"] = filter.Id;

                if (!string.IsNullOrEmpty(filter.Code))
                    query["Code"] = filter.Code;


                query["StartDate"] = filter.StartDate.ToString("yyyy/MM/dd");
                query["EndDate"] = filter.EndDate.ToString("yyyy/MM/dd");

                if (!string.IsNullOrEmpty(filter.Event))
                    query["Event"] = filter.Event;

                if (!string.IsNullOrEmpty(filter.User))
                    query["User"] = filter.User;

                // Paginación
                query["PageNumber"] = pagination.PageNumber.ToString();
                query["PageSize"] = pagination.PageSize.ToString();

                if (!string.IsNullOrEmpty(filter.DynamicQuery))
                    query["DynamicQuery"] = filter.DynamicQuery;

                if (!string.IsNullOrEmpty(filter.QueryParams))
                    query["QueryParams"] = filter.QueryParams;

                if (!string.IsNullOrEmpty(filter.SortProp))
                    query["SortProp"] = filter.SortProp;

                if (!string.IsNullOrEmpty(filter.SortDirection))
                    query["SortDirection"] = filter.SortDirection;

                query["ExactValue"] = filter.ExactValue.ToString();

                // Construir la cadena de la consulta (query string)
                var queryString = string.Join("&", query.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}"));

                // Crear la URL completa para la solicitud
                var requestUri = $"{BaseAdress}{string.Format(uri)}?{queryString}";

                // Realizar la solicitud HTTP
                var response = await _client.GetAsync(requestUri);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response != null && response.IsSuccessStatusCode)
                {
                    var jsonString3 = await response.Content.ReadAsStringAsync();
                    var objetos = JsonSerializer.Deserialize<T>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return objetos;
                }

                return default(T);
            }
            catch (Exception exception)
            {
                // Manejo de errores
                return default(T);
            }
        }



        protected async Task<HttpResponseMessage> HttpPostAsJsonAsync(string uri, object data)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseAdress}{uri}", data);
                var algo = response.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        protected async Task<HttpResponseMessage> HttpPutAsJsonAsync(string uri, object data)
        {
            try
            {
                HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseAdress}{uri}", data);
                return response;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}
