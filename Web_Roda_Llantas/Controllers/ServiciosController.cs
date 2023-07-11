using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;
using Web_Roda_Llantas.Models;

namespace Web_Roda_Llantas.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly IServiciosModel _serviciosModel;
        private readonly ITipoProductoModel _tipoProductoModel;
        private readonly IUtilitariosModel _utilitariosModel;

        public ServiciosController(IServiciosModel serviciosModel, IUtilitariosModel utilitariosModel, ITipoProductoModel tipoProductoModel, ITipoProductoModel ipoProductoModel)
        {
            _serviciosModel = serviciosModel;
            _tipoProductoModel = tipoProductoModel;
            _utilitariosModel = utilitariosModel;
        }

        [HttpGet]
        public IActionResult ConsultarServicios()
        {
            try
            {
                var datos = _serviciosModel.ConsultarServicios();
                ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();

                return View(datos);
            }
            catch (Exception ex)
            {
                int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
                return View("Error");
            }
        }


        [HttpPost]
        public IActionResult ActualizarServicios(ServiciosEntities entidad)
        {
            try
            {
                _serviciosModel.ActualizarServicios(entidad);
                ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
                return RedirectToAction("ConsultarServicios", "Servicios");
            }
            catch (Exception ex)
            {
                int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult ActualizarServicios(long q)
        {
            try
            {
                var datos = _serviciosModel.ConsultarServicio(q);
                ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
                return View(datos);
            }
            catch (Exception ex)
            {
                int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult RegistrarServicios(ServiciosEntities entidad)
        {
            try
            {
                var resultado = _serviciosModel.RegistrarServicios(entidad);
                ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();

                if (resultado > 0)
                {
                    return RedirectToAction("ConsultarServicios", "Servicios");
                }
                else
                {
                    return RedirectToAction("RegistrarServicios", "Servicios");
                }
            }
            catch (Exception ex)
            {
                int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult RegistrarServicios()
        {
            try
            {

                ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
                return View();
            }
            catch (Exception ex)
            {
                int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
                return View("Error");
            }
        }
    }
}

