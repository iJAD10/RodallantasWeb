using System.Net.Http.Headers;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;

namespace Web_Roda_Llantas.Models
{
    public class CarritoModel : ICarritoModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;


        public CarritoModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public bool RegistrarCompra(List<ProductosEntities> entidad)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Carrito/RegistrarCompra";

                string token = _contextAccessor.HttpContext.Session.GetString("Token")?.ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<bool>().Result;
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
                return false;
            }
        }
        public IEnumerable<CarritoListarEntities> ObtenerCarritoPorUsuario(int usuId)
        {
            using (var client = new HttpClient())
            {
                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + $"Carrito/ListarPorUsuario/{usuId}";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<IEnumerable<CarritoListarEntities>>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return new List<CarritoListarEntities>();
            }
        }
    }
}
