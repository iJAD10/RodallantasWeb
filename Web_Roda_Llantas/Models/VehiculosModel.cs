using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.ResponseCompression;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;
using System.Data.SqlClient;
using System.Numerics;

namespace Web_Roda_Llantas.Models
{
    public class VehiculosModel : IVehiculosModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public VehiculosModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public List<VehiculosEntities>? ConsultarVehiculos()
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Vehiculos/ConsultarVehiculos";

                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<List<VehiculosEntities>>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return new List<VehiculosEntities>();
            }
        }

        public VehiculosEntities? ConsultarVehiculo(long q)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Vehiculos/ConsultarVehiculo?q=" + q;

                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<VehiculosEntities>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return null;
            }
        }

        public VehiculosEntities? ConsultarPlaca(string q)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Vehiculos/ConsultarPlaca?q=" + q;

                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<VehiculosEntities>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return null;
            }
        }

        public int RegistrarVehiculos(VehiculosEntities entidad)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Vehiculos/RegistrarVehiculos";
                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<int>().Result;
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
                return 0;
            }
        }

        public void ActualizarVehiculos(VehiculosEntities entidad)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Vehiculos/ActualizarVehiculos";

                //Serializar convertir un objeto a json
                JsonContent body = JsonContent.Create(entidad);

                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.PutAsync(urlApi, body).Result;
            }
        }
    }
}
