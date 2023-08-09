using Microsoft.AspNetCore.Mvc;
using System;
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
        public IActionResult FinalizarCompra(int usuId)
        {
            try
            {
                _comprasModel.FinalizarCompra(usuId);

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
    }
}
