using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;

namespace Web_Roda_Llantas.Models
{
    public class BitacoraModel : IBitacoraModel
    {
        private readonly IConfiguration _configuration;

        public BitacoraModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void RegistrarBitacora(BitacoraEntities entidad)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("apiUrl:usuario").Value + "Bitacora/RegistrarBitacora";

                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage response = client.PostAsync(urlApi, body).Result;
            }
        }
    }
}
