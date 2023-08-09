using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;
using Web_Roda_Llantas.Models;
using static Web_Roda_Llantas.Enum.SweetAlertEnums;

namespace Web_Roda_Llantas.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioModel _usuarioModel;
        private readonly ITipoProductoModel _tipoProductoModel;
        private readonly IUtilitariosModel _utilitariosModel;

        public HomeController(ILogger<HomeController> logger, IUsuarioModel usuarioModel,ITipoProductoModel tipoProductoModel, IUtilitariosModel utilitariosModel)
        {
            _logger = logger;
            _usuarioModel = usuarioModel;
            _tipoProductoModel = tipoProductoModel;
            _utilitariosModel = utilitariosModel;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                HttpContext.Session.Clear();
                return View();
            }
            catch (Exception ex)
            {
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, 0);
                return View("Error");
            }
        }

        //[HttpGet]
        //[FiltroValidaSesion]
        //public IActionResult Principal()
        //{
        //    try
        //    {
        //        CarritoEntities carrito = new CarritoEntities();
        //        carrito.limpiarCarrito();
        //        ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
        //        _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
        //        return View("Error");
        //    }
        //}

        [HttpGet]
        [FiltroValidaSesion]
        public ActionResult PrincipalCliente() {
            ViewBag.OpcionesProductos = new SelectList(_tipoProductoModel.ConsultarTipoProducto(), "TP_Id", "TP_Nombre");
            return View(); }

		//[HttpPost]
		//public IActionResult Principal(UsuarioEntities entidad)
		//{
		//    try
		//    {
		//        var resultado = _usuarioModel.ValidarCredenciales(entidad);

		//        if (resultado != null)
		//        {
		//            HttpContext.Session.SetString("Nombre", resultado.Usu_Nombre);
		//            HttpContext.Session.SetString("Correo", resultado.Usu_Correo);
		//            HttpContext.Session.SetString("Token", resultado.Token);
		//            HttpContext.Session.SetString("Usu_Id", resultado.Usu_Id.ToString());
		//            return RedirectToAction("Principal", "Home");
		//        }
		//        else
		//        {
		//            Alert("Error de validación.", "Por favor verifique sus credenciales.", NotificationType.error);
		//            return View("Index");
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        _utilitariosModel.RegistrarBitacora(ex, ControllerContext, 0);
		//        return View("Error");
		//    }
		//}

		[HttpPost]
		public IActionResult Principal(UsuarioEntities entidad)
		{
			try
			{
				var resultado = _usuarioModel.ValidarCredenciales(entidad);

				if (resultado != null)
				{
					HttpContext.Session.SetString("Nombre", resultado.Usu_Nombre);
                    HttpContext.Session.SetString("Correo", resultado.Usu_Correo);
                    HttpContext.Session.SetString("Usu_Num_Carrito", resultado.Usu_Num_Carrito.ToString());
                    HttpContext.Session.SetString("Token", resultado.Token);
					HttpContext.Session.SetString("Usu_Id", resultado.Usu_Id.ToString());

					if (resultado.UR_Rol_Id == 1) // Si el usuario es administrador
					{
						return RedirectToAction("Principal", "Home");
					}
					else if (resultado.UR_Rol_Id == 2) // Si el usuario es cliente
					{
						return RedirectToAction("PrincipalCliente", "Home");
					}
					// Añade un mensaje para los casos en los que Rol_Id no sea ni 1 ni 2
					else
					{
						Alert("Error de validación.", "Rol de usuario no reconocido.", NotificationType.error);
					}
				}
				else
				{
					Alert("Error de validación.", "Por favor verifique sus credenciales.", NotificationType.error);
				}
			}
			catch (Exception ex)
			{
				_utilitariosModel.RegistrarBitacora(ex, ControllerContext, 0);
			}
			// Si ninguna de las condiciones anteriores se cumple, regresa a la vista de inicio
			return View("Index");
		}

		[HttpGet]
        [FiltroValidaSesion]
        public IActionResult CerrarSesion()
        {
            try
            {
                HttpContext.Session.Clear();
                CarritoEntities carrito = new CarritoEntities();
                //carrito.limpiarCarrito();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
                return View("ErrorSistema");
            }
        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, 0);
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult RegistrarUsuario(UsuarioEntities entidad)
        {
            try
            {
                var resultado = _usuarioModel.RegistrarUsuario(entidad);

                if (resultado > 0)
                    Alert("Usuario Registrado.", "El nuevo usuario se registro correctamente.", NotificationType.success);
                else
                    Alert("Error en el Registro.", "Hubo un error registrando al usuario, por favor intentelo más tarde.", NotificationType.error);

                return View("Index");
            }
            catch (Exception ex)
            {
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, 0);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult RecuperarContrasenna()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, 0);
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult RecuperarContrasenna(UsuarioEntities entidad)
        {
            try
            {
                _usuarioModel.RecuperarContrasenna(entidad);
                Alert("Se proceso la recuperación contraseña.", "Por favor revice su buzón de correo electrónico.", NotificationType.info);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, 0);
                return View("Error");
            }
        }

        public IActionResult BuscarExisteCorreo(string Usu_Correo)
        {
            return Json(_usuarioModel.BuscarExisteCorreo(Usu_Correo));
        }
    }
}