
using Microsoft.AspNetCore.Mvc;
using Web_Roda_Llantas.Entities;
using Web_Roda_Llantas.Interfaces;
using Web_Roda_Llantas.Models;
using static Web_Roda_Llantas.Enum.SweetAlertEnums;

namespace Web_Roda_Llantas.Controllers
{
	public class ProductosController : BaseController
	{
		private readonly IProductosModel _productosModel;
		private readonly ITipoProductoModel _tipoProductoModel;
		private readonly IUtilitariosModel _utilitariosModel;





		private static string _tipoProducto;

		public ProductosController(IProductosModel productosModel, ITipoProductoModel tipoProductoModel, IUtilitariosModel utilitariosModel)

		{
			_productosModel = productosModel;
			_tipoProductoModel = tipoProductoModel;
			_utilitariosModel = utilitariosModel;

		}

        [HttpPost]
        public IActionResult RegistrarProductos(ProductosEntities entidad)
        {
            try
            {
                var resultado = _productosModel.RegistrarProductos(entidad);
                ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();

                if (resultado > 0)
                {
                    return RedirectToAction("ConsultarProductos", "Productos");
                }
                else
                {
                    return RedirectToAction("RegistrarProductos", "Productos");
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
		public IActionResult RegistrarProductos()
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
        public IActionResult ConsultarProductos()
        {
            try
            {
                var datos = _productosModel.ConsultarProductos();
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
		public IActionResult ActualizarProductos(ProductosEntities entidad)
		{
			try
			{
				_productosModel.ActualizarProductos(entidad);
				ViewBag.OpcionesProductos = _tipoProductoModel.ConsultarTipoProducto();
				return RedirectToAction("ActualizarProductos", "Productos");
			}
			catch (Exception ex)
			{
				int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
				_utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
				return View("Error");
			}
		}

		public ActionResult AgregarCarrito(int id_producto)
		{
			CarritoEntities carrito = new CarritoEntities();
			if (carrito.agregarProducto(_productosModel.ConsultarProductoXID(id_producto)))
			{
				Alert("Producto Agregado", "El producto a sido agregado al carrito", NotificationType.info);
			}
			else
			{
				Alert("Error al agregar producto", "La cantidad de producto en el carrito supera a la cantidad en stock", NotificationType.error);
			}

			return RedirectToAction("ConsultarProductos", "Productos");
		}

	}

}
