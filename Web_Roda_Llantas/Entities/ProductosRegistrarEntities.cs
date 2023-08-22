namespace Web_Roda_Llantas.Entities
{
    public class ProductosRegistrarEntities
    {
        public int Prod_Id { get; set; }
        public string Prod_Marca { get; set; } = string.Empty;
        public string Prod_Nombre { get; set; }
        public string Prod_Descripcion { get; set; }
        public double Prod_Precio { get; set; }
        public int Prod_CantStock { get; set; }
        public int Prod_Proveedor_Id { get; set; }
        public int Prod_TipoProducto_Id { get; set; }
        public string Prod_Url_Img { get; set; }
    }
}
