using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;

namespace Web_Roda_Llantas.Models
{
	public class ReservacionesModel : IReservacionesModel
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _contextAccessor;

		public ReservacionesModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
		{
			_configuration = configuration;
			_contextAccessor = contextAccessor;
		}
		
		public int RegistrarReservacion(ReservacionesEntities entidad)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Reservaciones/RegistrarReservacion";

				JsonContent body = JsonContent.Create(entidad);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (response.IsSuccessStatusCode)
					return response.Content.ReadFromJsonAsync<int>().Result;
				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return 0;
			}
		}

		public List<ReservacionesEntities>? ConsultarReservaciones()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Reservaciones/ConsultarReservaciones";

				string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
					return response.Content.ReadFromJsonAsync<List<ReservacionesEntities>>().Result;

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return new List<ReservacionesEntities>();
			}
		}

		public List<ReservacionesEntities>? DetallesReservacion(long q)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Reservaciones/DetallesReservacion?q=" + q;

				string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
					return response.Content.ReadFromJsonAsync<List<ReservacionesEntities>>().Result;

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return null;
			}
		}

		public int CambiarCompletado(ReservacionesEntities entidad)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Reservaciones/CambiarCompletado";

				JsonContent body = JsonContent.Create(entidad);

				string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
				HttpResponseMessage response = client.PutAsync(urlApi, body).Result;

				if (response.IsSuccessStatusCode)
					return response.Content.ReadFromJsonAsync<int>().Result;

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepcion Web Api: " + response.Content.ReadAsStringAsync().Result);
				return 0;


			}
		}
	}
}
