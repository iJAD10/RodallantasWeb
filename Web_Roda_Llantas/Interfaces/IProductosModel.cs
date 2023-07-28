using Web_Roda_Llantas.Entities;

namespace Web_Roda_Llantas.Interfaces
{
    public interface IProductosModel
    {
        public List<ProductosEntities>? ConsultarProductos();
        public ProductosEntities ConsultarProductoXID(int id_producto);
        public List<ProductosEntities>? ConsultarProductosXIDTipoProducto(string Prod_Id);

        public void ActualizarProductos(ProductosEntities entidad);

        public int RegistrarProductos(ProductosRegistrarEntities entidad);
    }
}
