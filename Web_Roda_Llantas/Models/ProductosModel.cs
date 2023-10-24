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
        public List<ProductosEntities>? ConsultarProductosConStock()
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Productos/ConsultarProductosConStock";

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
        public ProductosEntities ConsultarProductoXID(int Id)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Productos/ConsultarProductoXID?Id=" + Id;

                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<ProductosEntities>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return null;
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
        public int RegistrarProductos(ProductosRegistrarEntities entidad)
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

        public void AgregarProductoACarrito(int usuId, int prodId, int cantidad)
        {
            using (var client = new HttpClient())
            {
                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Productos/AgregarProductoACarrito";

                var request = new CarritoRequest { Usu_Id = usuId, Prod_Id = prodId, Cantidad = cantidad };
                JsonContent body = JsonContent.Create(request);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
            }
        }

    }
    public class CarritoRequest
    {
        public int Usu_Id { get; set; }
        public int Prod_Id { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
        public string CorreoUsuario { get; set; }

    }
}
