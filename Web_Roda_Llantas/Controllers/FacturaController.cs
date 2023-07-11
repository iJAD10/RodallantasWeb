using Microsoft.AspNetCore.Mvc;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;
using Web_Roda_Llantas.Models;

namespace Web_Roda_Llantas.Controllers
{
    public class FacturaController : Controller
    {
        private readonly IFacturaModel _facturaModel;
        private readonly ITipoProductoModel _tipoProductoModel;
        private readonly IUtilitariosModel _utilitariosModel;

        public FacturaController(IFacturaModel facturaModel, ITipoProductoModel tipoProductoModel, IUtilitariosModel utilitariosModel)
        {
            _facturaModel = facturaModel;
            _tipoProductoModel = tipoProductoModel;
            _utilitariosModel = utilitariosModel;
        }

        [HttpGet]
        public IActionResult ConsultarFactura()
        {
            try
            {
                FacturaEntities Factura = _facturaModel.ConsultarUltimaCompra();
                CarritoEntities carrito = new CarritoEntities();
                Factura.ListaProductos = carrito.consultarCarrito();
                ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
                return View(Factura);
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
