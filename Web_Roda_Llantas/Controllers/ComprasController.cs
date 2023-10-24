using Microsoft.AspNetCore.Mvc;
using System;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;
using Web_Roda_Llantas.Models;

namespace Web_Roda_Llantas.Controllers
{
    public class ComprasController : BaseController

    {
        private readonly ICompras _comprasModel;
        private readonly IUtilitariosModel _utilitariosModel;

        public ComprasController(ICompras comprasModel, IUtilitariosModel utilitariosModel)
        {
            _comprasModel = comprasModel;
            _utilitariosModel = utilitariosModel;
        }

        [HttpPost]
        public IActionResult FinalizarCompra(int usuId, int total, string correoUsuario)
        {
            try
            {
                _comprasModel.FinalizarCompra(usuId, total,correoUsuario);

                TempData["SuccessMessage"] = "Producto agregado correctamente";

                // Redirige a la vista donde quieres mostrar el mensaje.
                return RedirectToAction("Confirmacion", "Compras");
            }
            catch (Exception ex)
            {
                int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Confirmacion()
        {
            // Aquí puedes cargar cualquier dato relevante que desees mostrar al usuario.
            return View();
        }

        [HttpGet]
        public ActionResult Listar()
        {
            var lista = _comprasModel.Listar();
            return View(lista);
        }

        [HttpGet]
        public IActionResult ListarDetalleOrdenPorId(int orden_Id)
        {
            var items = _comprasModel.ListarDetalleOrdenPorId(orden_Id);
            return View(items);
        }

        [HttpPost] 
        public IActionResult CambiarCompletado(int orden_Id)
        {
            try
            {
                _comprasModel.CambiarCompletado(orden_Id);

                // Puedes agregar un mensaje de éxito usando TempData u otra técnica de tu preferencia.
                TempData["Message"] = "Estado de la Orden cambiado con éxito!";

                // Luego redirigir a la vista donde quieres que vaya el usuario después de cambiar el estado.
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                // Maneja la excepción mostrando un mensaje de error al usuario o redirigiéndolo a una página de error.
                ModelState.AddModelError("", "Error al cambiar el estado de la orden: " + ex.Message);
                return View("Error"); // Asume que tienes una vista "Error".
            }
        }
    }
}
