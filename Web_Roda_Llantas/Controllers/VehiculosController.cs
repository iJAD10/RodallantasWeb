using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;
using Web_Roda_Llantas.Models;

namespace Web_Roda_Llantas.Controllers
{
    public class VehiculosController : Controller
    {

        private readonly IVehiculosModel _vehiculosModel;
        private readonly ITipoProductoModel _tipoProductoModel;
        private readonly IUtilitariosModel _utilitariosModel;

        public VehiculosController(IVehiculosModel vehiculosModel, IUtilitariosModel utilitariosModel1, ITipoProductoModel tipoProductoModel, IUtilitariosModel utilitariosModel)
        {
            _vehiculosModel = vehiculosModel;
            _tipoProductoModel = tipoProductoModel;
            _utilitariosModel = utilitariosModel;
        }

        [HttpGet]
        public IActionResult ConsultarVehiculos()
        {
            try
            {
                var datos = _vehiculosModel.ConsultarVehiculos();
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
        public IActionResult ActualizarVehiculos(VehiculosEntities entidad)
        {
            try
            {
                _vehiculosModel.ActualizarVehiculos(entidad);
                ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
                return RedirectToAction("ActualizarVehiculos", "Vehiculos");
            }
            catch (Exception ex)
            {
                int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
                return View("Error");
            }
        }

      
        [HttpPost]
        public IActionResult RegistrarVehiculos(VehiculosEntities entidad)
        {
            try
            {
                var resultado = _vehiculosModel.RegistrarVehiculos(entidad);
                ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();

                if (resultado > 0)
                {
                    return RedirectToAction("ConsultarVehiculos", "Vehiculos");
                }
                else
                {
                    return RedirectToAction("RegistrarVehiculos", "Vehiculos");
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
        public IActionResult ActualizarVehiculos(long q)
        {
            try
            {
                var datos = _vehiculosModel.ConsultarVehiculo(q);
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


        [HttpGet]
        public IActionResult RegistrarVehiculos( )
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

        [HttpGet]
        public JsonResult ConsultarPlaca(string placa)
        {
            bool existe = false;
            var datos = _vehiculosModel.ConsultarPlaca(placa);

            if( datos != null )
                existe = true;

            
            return Json(new { resultado = existe });
        }
    }

    }

