using Microsoft.AspNetCore.Mvc;
using Web_Roda_Llantas.Entities;

namespace Web_Roda_Llantas.Interfaces
{
    public interface IUtilitariosModel
    {
        public void RegistrarBitacora(Exception ex, ControllerContext contexto, int Usu_Id);
        public CountEntities Count();
    }
}
