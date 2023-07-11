using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;
using Web_Roda_Llantas.Models;

namespace Web_Roda_Llantas.Controllers
{
    [FiltroValidaSesion]
    public class UsuariosController : Controller
    {

        private readonly IUsuarioModel _usuariosModel;
        private readonly ITipoProductoModel _tipoProductoModel;
        private readonly IUtilitariosModel _utilitariosModel;

        public UsuariosController(IUsuarioModel usuariosModel, ITipoProductoModel tipoProductoModel, IUtilitariosModel utilitariosModel)
        {
            _usuariosModel = usuariosModel;
            _tipoProductoModel = tipoProductoModel;
            _utilitariosModel = utilitariosModel;
        }

        [HttpGet]
        public IActionResult ConsultarUsuarios()
        {
            try
            {
                var datos = _usuariosModel.ConsultarUsuarios();
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
        public IActionResult InactivarUsuario(int Usu_Id)
        {
            UsuarioEntities entidad = new UsuarioEntities();
            entidad.Usu_Id = Usu_Id;

            return Json(_usuariosModel.InactivarUsuario(entidad));
        }

        [HttpPost]
        public IActionResult ActualizarUsuario (UsuarioEntities entidad)
        {
            try
            {
                _usuariosModel.ActualizarUsuario(entidad);
                ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
                return RedirectToAction("ConsultarUsuarios", "Usuarios");
            }
            catch (Exception ex)
            {
                int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult ActualizarUsuario(long q)
        {
            try
            {
                var datos = _usuariosModel.ConsultarUsuario(q);
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
    }
}