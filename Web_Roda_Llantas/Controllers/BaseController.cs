using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Web_Roda_Llantas.Enum.SweetAlertEnums;

namespace Web_Roda_Llantas.Controllers
{
    public class BaseController : Controller
    {
        public void Alert(string tittle,string message, NotificationType notificationType)
        {
            var msg = "<script>swal('"+tittle+ "', '" + message + "', '"+ notificationType.ToString()+ "', {button: 'Aceptar',});</script>";
            TempData["notification"] = msg;
        }
    }
}
