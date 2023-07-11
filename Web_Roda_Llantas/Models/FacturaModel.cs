using System.Net.Http.Headers;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;

namespace Web_Roda_Llantas.Models
{
    public class FacturaModel : IFacturaModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;


        public FacturaModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }
        public FacturaEntities ConsultarUltimaCompra()
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Factura/ConsultarUltimaCompra";

                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<FacturaEntities>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return new FacturaEntities();
            }
        }
    }
}
