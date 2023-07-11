using Web_Roda_Llantas.Controllers;
using static Web_Roda_Llantas.Enum.SweetAlertEnums;

namespace Web_Roda_Llantas.Entities
{
    public class CarritoEntities : BaseController
    {
        public static List<ProductosEntities> ListaProductos { get; set; } = new List<ProductosEntities>();

        public bool agregarProducto(ProductosEntities producto) {
            bool existeProducto = false;
            bool puedeAgregar = true; 
            foreach (ProductosEntities v_producto in ListaProductos) {
                if (v_producto.Prod_Id == producto.Prod_Id)
                {
                    existeProducto = true;
                    if (producto.Prod_CantStock > v_producto.Prod_Cantidad)
                    {
                        v_producto.Prod_Cantidad++;
                        v_producto.Prod_total = v_producto.Prod_Precio * v_producto.Prod_Cantidad;
                    }
                    else
                    {
                        puedeAgregar = false;
                        break;
                    }

                }

            }

            
            if (!existeProducto) {
                producto.Prod_Cantidad = 1;
                producto.Prod_total = producto.Prod_Precio * producto.Prod_Cantidad;
                ListaProductos.Add(producto);
            }
            return puedeAgregar;
        }
        public List<ProductosEntities> consultarCarrito()
        {
            return ListaProductos;
        }

        public void eliminarProducto(int Prod_Id)
        {
            foreach (ProductosEntities v_producto in ListaProductos) {
                if (Prod_Id == v_producto.Prod_Id) { 
                    ListaProductos.Remove(v_producto);
                    break;
                }
            
            }
            
        }
        public void limpiarCarrito()
        {
            ListaProductos.Clear();
        }
    }
}
