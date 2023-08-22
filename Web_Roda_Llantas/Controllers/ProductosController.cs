
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
		private readonly IProveedoresModel _proveedoresModel;

		public ProductosController(IProductosModel productosModel, ITipoProductoModel tipoProductoModel, IUtilitariosModel utilitariosModel, IProveedoresModel proveedoresModel)

		{
			_productosModel = productosModel;
			_tipoProductoModel = tipoProductoModel;
			_utilitariosModel = utilitariosModel;
			_proveedoresModel = proveedoresModel;



		}

		[HttpPost]
		public IActionResult RegistrarProductos(ProductosRegistrarEntities entidad)
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
				ViewBag.OpcionesProveedores = new SelectList(_proveedoresModel.ConsultarProveedorXNombre(), "Prov_Id", "Prov_Nombre_Empresa");
				ViewBag.OpcionesProductos = new SelectList(_tipoProductoModel.ConsultarTipoProducto(), "TP_Id", "TP_Nombre");
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

		//public ActionResult AgregarCarrito(int id_producto)
		//{
		//	CarritoEntities carrito = new CarritoEntities();
		//	if (carrito.agregarProducto(_productosModel.ConsultarProductoXID(id_producto)))
		//	{
		//		Alert("Producto Agregado", "El producto a sido agregado al carrito", NotificationType.info);
		//	}
		//	else
		//	{
		//		Alert("Error al agregar producto", "La cantidad de producto en el carrito supera a la cantidad en stock", NotificationType.error);
		//	}

		//	return RedirectToAction("ConsultarProductos", "Productos");
		//}

		//Cliente

		[HttpGet]
		public IActionResult ConsultarProductosCliente()
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

        [HttpGet]
        public IActionResult ConsultarProductosConStock()
        {
            try
            {
                var datos = _productosModel.ConsultarProductosConStock();
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
		public IActionResult ConsultarProductoXID(int Id)
		{
			try
			{
				var datos = _productosModel.ConsultarProductoXID(Id);
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


        //    [HttpPost]
        //    public IActionResult AgregarProductoACarrito(int usuId, int prodId, int cantidad)
        //    {
        //        try
        //        {
        //HttpContext.Session.GetString("Usu_Num_Carrito");
        //            _productosModel.AgregarProductoACarrito(usuId, prodId, cantidad);

        //            TempData["SuccessMessage"] = "Producto agregado correctamente";

        //            // Redirige a la vista donde quieres mostrar el mensaje.
        //            return RedirectToAction("ConsultarProductosCliente", "Productos");
        //        }
        //        catch (Exception ex)
        //        {
        //            int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
        //            _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
        //            return View("Error");
        //        }
        //    }


        //Metodo con AJAX
        [HttpPost]
        public IActionResult AgregarProductoACarrito(int usuId, int prodId, int cantidad)
        {
            try
            {
                _productosModel.AgregarProductoACarrito(usuId, prodId, cantidad);

                // Suponiendo que tienes un método que te da la cantidad actual en el carrito.
                int nuevoNumero = int.Parse(HttpContext.Session.GetString("Usu_Num_Carrito")) + 1;
                HttpContext.Session.SetString("Usu_Num_Carrito", nuevoNumero.ToString());

                return Json(new { success = true, nuevoNumero = nuevoNumero });
            }
            catch (Exception ex)
            {
                int Usu_Id = int.Parse(HttpContext.Session.GetString("Usu_Id"));
                _utilitariosModel.RegistrarBitacora(ex, ControllerContext, Usu_Id);
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

}
