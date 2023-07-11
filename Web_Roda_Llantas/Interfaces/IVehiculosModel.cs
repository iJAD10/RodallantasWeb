using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Roda_Llantas.Controllers;
using Web_Roda_Llantas.Entities;

namespace Web_Roda_Llantas.Interfaces
{
    public interface IVehiculosModel
    {
        public List<VehiculosEntities>? ConsultarVehiculos();

        public void ActualizarVehiculos(VehiculosEntities entidad);

        public int RegistrarVehiculos(VehiculosEntities entidad);

        public VehiculosEntities? ConsultarVehiculo(long q);

        public VehiculosEntities? ConsultarPlaca(string q);
    }
}
