using System.Net.Http.Headers;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;

namespace Web_Roda_Llantas.Models
{
    public class ServiciosModel : IServiciosModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public ServiciosModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public List<ServiciosEntities>? ConsultarServicios()
        {
            using (var client = new HttpClient())
            {
                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Servicios/ConsultarServicios";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<List<ServiciosEntities>>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return new List<ServiciosEntities>();
            }
        }

        public ServiciosEntities? ConsultarServicio(long q)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Servicios/ConsultarServicio?q=" + q;

                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<ServiciosEntities>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return null;
            }
        }

        public int RegistrarServicios(ServiciosEntities entidad)
        {
            using (var client = new HttpClient())
            {
                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Servicios/RegistrarServicios";

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

        public void ActualizarServicios(ServiciosEntities entidad)
        {
            using (var client = new HttpClient())
            {
                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Servicios/ActualizarServicios";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage response = client.PutAsync(urlApi, body).Result;
            }
        }
    }
}
