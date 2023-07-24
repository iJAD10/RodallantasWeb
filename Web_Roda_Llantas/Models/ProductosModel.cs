using System.Net.Http.Headers;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;

namespace Web_Roda_Llantas.Models
{
    public class ProductosModel : IProductosModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProductosModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public List<ProductosEntities>? ConsultarProductos()
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Productos/ConsultarProductos";

                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<List<ProductosEntities>>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return new List<ProductosEntities>();
            }
        }
        public ProductosEntities? ConsultarProductoXID(int Prod_Id)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Productos/ConsultarProductoXID";

                //Serializar convertir un objeto a json
                JsonContent body = JsonContent.Create(new TipoProductoEntities { TP_Id = Prod_Id });

                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.PutAsync(urlApi, body).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<ProductosEntities>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return new ProductosEntities();
            }
        }

        public List<ProductosEntities>? ConsultarProductosXIDTipoProducto(string Prod_Id)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Productos/ConsultarProductosXIDTipoProducto";

                //Serializar convertir un objeto a json
                JsonContent body = JsonContent.Create(new TipoProductoEntities { TP_Nombre = Prod_Id });

                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.PutAsync(urlApi, body).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<List<ProductosEntities>>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return new List<ProductosEntities>();
            }
        }
        public int RegistrarProductos(ProductosEntities entidad)
        {
            using (var client = new HttpClient())
            {
                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Productos/RegistrarProductos";

                JsonContent body = JsonContent.Create(entidad);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<int>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return 0;
            }
        }

        public void ActualizarProductos(ProductosEntities entidad)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Productos/ActualizarProductos";

                //Serializar convertir un objeto a json
                JsonContent body = JsonContent.Create(entidad);

                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.PutAsync(urlApi, body).Result;
            }
        }
    }
}
