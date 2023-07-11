using Microsoft.AspNetCore.Mvc;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;

namespace Web_Roda_Llantas.Models
{
    public class UtilitariosModel : IUtilitariosModel
    {
        private readonly IBitacoraModel _bitacoraModel;

        public UtilitariosModel(IBitacoraModel bitacoraModel)
        {
            _bitacoraModel = bitacoraModel;
        }

        public void RegistrarBitacora(Exception ex, ControllerContext contexto, int Usu_Id)
        {
            BitacoraEntities bitacora = new BitacoraEntities();
            bitacora.Bit_Detalle = ex.Message;
            bitacora.Bit_Origen = contexto.ActionDescriptor.ControllerName + "-" + contexto.ActionDescriptor.ActionName;
            bitacora.Bit_Usu_Id = Usu_Id;
            _bitacoraModel.RegistrarBitacora(bitacora);
        }
    }
}
