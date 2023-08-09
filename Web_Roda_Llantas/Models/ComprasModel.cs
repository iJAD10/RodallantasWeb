using System.Net.Http.Headers;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;

namespace Web_Roda_Llantas.Models
{
    public class ComprasModel: ICompras
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public ComprasModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public void FinalizarCompra(int usuId)
        {
            using (var client = new HttpClient())
            {
                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Compras/FinalizarCompra";

                var request = new CarritoRequest { Usu_Id = usuId };
                JsonContent body = JsonContent.Create(request);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
            }
        }
    }
    public class Request
    {
        public int Usu_Id { get; set; }

    }

}
