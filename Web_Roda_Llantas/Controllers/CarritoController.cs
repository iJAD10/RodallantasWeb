
using Microsoft.AspNetCore.Mvc;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;
using static Web_Roda_Llantas.Enum.SweetAlertEnums;

namespace Web_Roda_Llantas.Controllers
{
    public class CarritoController : BaseController
    {
        private readonly ICarritoModel _carritoModel;
        private readonly ITipoProductoModel _tipoProductoModel;
        private readonly IUtilitariosModel _utilitariosModel;

        public CarritoController(ICarritoModel carritoModel, ITipoProductoModel tipoProductoModel, IUtilitariosModel utilitariosModel)
        {
            _carritoModel = carritoModel;
            _tipoProductoModel = tipoProductoModel;
            _utilitariosModel = utilitariosModel;
        }
        //[HttpGet]
        //public IActionResult ConsultarCarrito()
        //{
        //    try
        //    {
        //        CarritoEntities carrito = new CarritoEntities();
        //        var datos = carrito.consultarCarrito();
        //        ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
        //        return datos.Count==0?  View(null): View(datos);
        //    }
        //    catch (Exception ex)
        //    {
        //        int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
        //        _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
        //        return View("Error");
        //    }
        //}

        //[HttpGet]
        //public IActionResult EliminarCarrito(int Prod_Id)
        //{
        //    try
        //    {
        //        CarritoEntities carrito = new CarritoEntities();
        //        carrito.eliminarProducto(Prod_Id);
        //        ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
        //        Alert("Eliminar producto", "El producto fue eliminado del carrito", NotificationType.warning);
        //        return RedirectToAction("ConsultarCarrito", "Carrito");
        //    }
        //    catch (Exception ex)
        //    {
        //        int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
        //        _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
        //        return View("Error");
        //    }
        //}

        //public IActionResult FinalizarCompra()
        //{
        //    try
        //    {
        //        CarritoEntities carrito = new CarritoEntities();
        //        _carritoModel.RegistrarCompra(carrito.consultarCarrito());
        //        return RedirectToAction("ConsultarFactura", "Factura");
        //    }
        //    catch (Exception ex)
        //    {
        //        int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
        //        _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
        //        return View("Error");
        //    }
        //}

        public IActionResult Listar()
        {
            
            int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
            var items = _carritoModel.ObtenerCarritoPorUsuario(Usu_Id);
            return View(items);
        }

    }
}
