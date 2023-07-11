using Microsoft.AspNetCore.Mvc;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;
using Web_Roda_Llantas.Models;

namespace Web_Roda_Llantas.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly IProveedoresModel _proveedoresModel;
        private readonly ITipoProductoModel _tipoProductoModel;
        private readonly IUtilitariosModel _utilitariosModel;

        public ProveedoresController(IProveedoresModel proveedoresModel, ITipoProductoModel tipoProductoModel, IUtilitariosModel utilitariosModel)
        {
            _proveedoresModel = proveedoresModel;
            _tipoProductoModel = tipoProductoModel;
            _utilitariosModel = utilitariosModel;
        }

        [HttpPost]
        public IActionResult ActualizarProveedores(ProveedoresEntities entidad)
        {
            try
            {
                _proveedoresModel.ActualizarProveedores(entidad);
                ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
                return RedirectToAction("ActualizarProveedores", "Proveedores");
            }
            catch (Exception ex)
            {
                int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult ActualizarProveedores(long q)
        {
            try
            {
                var datos = _proveedoresModel.ConsultarProveedor(q);
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
        public IActionResult ConsultarProveedores()
        {
            try
            {
                var datos = _proveedoresModel.ConsultarProveedores();
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
        public IActionResult InactivarProveedor(int Prov_Id)
        {
            ProveedoresEntities entidad = new ProveedoresEntities();
            entidad.Prov_Id = Prov_Id;

            return Json(_proveedoresModel.InactivarProveedor(entidad));
        }

        [HttpPost]
        public IActionResult RegistrarProveedores(ProveedoresEntities entidad)
        {
            try
            {
                var datos = _proveedoresModel.RegistrarProveedores(entidad);
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
        public IActionResult RegistrarProveedores()
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