using Microsoft.AspNetCore.Mvc;

namespace Web_Roda_Llantas.Interfaces
{
    public interface IUtilitariosModel
    {
        public void RegistrarBitacora(Exception ex, ControllerContext contexto, int Usu_Id);
    }
}
