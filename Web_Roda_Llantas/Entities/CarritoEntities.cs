using Web_Roda_Llantas.Controllers;
using static Web_Roda_Llantas.Enum.SweetAlertEnums;

namespace Web_Roda_Llantas.Entities
{
    public class CarritoEntities
    {
        public long IdCarrito { get; set; }
        public int Prod_Id { get; set; }
        public int Usu_Id { get; set; }
        public int Cantidad { get; set; }
    }
}