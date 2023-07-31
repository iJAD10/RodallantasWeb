using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;
using Web_Roda_Llantas.Models;

namespace Web_Roda_Llantas.Controllers
{
	public class ReservacionesController : Controller
	{
		private readonly IReservacionesModel _reservacionesModel;
		private readonly IUtilitariosModel _utilitariosModel;
        private readonly ITipoProductoModel _tipoProductoModel;
        private readonly IServiciosModel _serviciosModel;

        public ReservacionesController(IReservacionesModel reservacionesModel, IUtilitariosModel utilitariosModel, ITipoProductoModel tipoProductoModel, IServiciosModel serviciosModel)
		{
			_reservacionesModel = reservacionesModel;
			_utilitariosModel = utilitariosModel;
            _tipoProductoModel = tipoProductoModel;
            _serviciosModel = serviciosModel;
        }

		[HttpGet]
		public IActionResult RegistrarReservacion()
		{
			try
            {
                ViewBag.Servicios = _serviciosModel.ConsultarServicios();
                ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
                return View();
			}
			catch (Exception ex)
			{
				_utilitariosModel.RegistrarBitacora(ex, ControllerContext, 0);
				return View("Error");
			}
		}
		
		[HttpPost]
		public IActionResult RegistrarReservacion(ReservacionesEntities entidad)
        {
            try
            {
                var resultado = _reservacionesModel.RegistrarReservacion(entidad);
                if (resultado > 0)
                    ViewBag.mensaje = "Se registró su reservación de forma exitosa";
                else
					ViewBag.mensaje = "No se pudo registrar la reservación";

                return RegistrarReservacion();
            }
			catch (Exception ex)
			{
				_utilitariosModel.RegistrarBitacora(ex, ControllerContext, 0);
                ViewBag.mensaje = "No se pudo registrar la reservación";

                return RegistrarReservacion();
            }
		}

		[HttpGet]
		public IActionResult ConsultarReservaciones()
		{
			try
			{
				var datos = _reservacionesModel.ConsultarReservaciones();
				ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
				return View(datos);
			}
			catch (Exception ex)
			{
				_utilitariosModel.RegistrarBitacora(ex, ControllerContext, 0);
				return View("Error");
			}
		}

		[HttpGet]
		public IActionResult DetallesReservacion(long q)
		{
			try
			{
				var datos = _reservacionesModel.DetallesReservacion(q);
				ViewBag.Nombre = datos[0].Usu_Nombre;
				ViewBag.Cedula = datos[0].Usu_Cedula;
				ViewBag.Correo = datos[0].Usuario_Correo;
				ViewBag.Completado = datos[0].Res_Completada;
				ViewBag.Estado = datos[0].Res_Estado;
				ViewBag.Vehiculo = datos[0].Veh_Placa;
				ViewBag.Reservacion = datos[0].Res_Id;
				ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
				return View(datos);
			}
			catch (Exception ex)
			{
				int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
				_utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
				return View(ex);
			}
		}

		[HttpGet]
		public IActionResult CambiarCompletado(int reservacion)
		{

			ReservacionesEntities entidad = new ReservacionesEntities();
			entidad.Res_Id = reservacion;

			return Json(_reservacionesModel.CambiarCompletado(entidad));

		}

        //Cliente
        [HttpGet]
        public IActionResult RegistrarVehiculoYReservacion()
        {
            try
            {
                ViewBag.Servicios = _serviciosModel.ConsultarServicios();
                ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
                return View();
            }
            catch (Exception ex)
            {
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, 0);
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult RegistrarVehiculoYReservacion(ReservacionesEntities entidad)
        {
            try
            {
                var resultado = _reservacionesModel.RegistrarVehiculoYReservacion(entidad);
                if (resultado > 0)
                    ViewBag.mensaje = "Se registró su reservación de forma exitosa";
                else
                    ViewBag.mensaje = "No se pudo registrar la reservación";

                return RegistrarVehiculoYReservacion();
            }
            catch (Exception ex)
            {
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, 0);
                ViewBag.mensaje = "No se pudo registrar la reservación";

                return RegistrarReservacion();
            }
        }
    }
}
