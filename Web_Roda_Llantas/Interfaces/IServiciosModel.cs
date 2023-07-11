using Web_Roda_Llantas.Entities;

namespace Web_Roda_Llantas.Interfaces
{
    public interface IServiciosModel
    {
        public List<ServiciosEntities>? ConsultarServicios();
        public void ActualizarServicios(ServiciosEntities entidad);
        public int RegistrarServicios(ServiciosEntities entidad);
        public ServiciosEntities? ConsultarServicio(long q);
    }
}
