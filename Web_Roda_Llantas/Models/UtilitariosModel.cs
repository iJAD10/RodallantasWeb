using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;

namespace Web_Roda_Llantas.Models
{
    public class UtilitariosModel : IUtilitariosModel
    {
        private readonly IBitacoraModel _bitacoraModel;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public UtilitariosModel(IBitacoraModel bitacoraModel, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _bitacoraModel = bitacoraModel;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public void RegistrarBitacora(Exception ex, ControllerContext contexto, int Usu_Id)
        {
            BitacoraEntities bitacora = new BitacoraEntities();
            bitacora.Bit_Detalle = ex.Message;
            bitacora.Bit_Origen = contexto.ActionDescriptor.ControllerName + "-" + contexto.ActionDescriptor.ActionName;
            bitacora.Bit_Usu_Id = Usu_Id;
            _bitacoraModel.RegistrarBitacora(bitacora);
        }

        public CountEntities Count()
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Compras/Count";

                string token = _contextAccessor.HttpContext.Session.GetString("Token").ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<CountEntities>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return new CountEntities();
            }
        }
    }
}
