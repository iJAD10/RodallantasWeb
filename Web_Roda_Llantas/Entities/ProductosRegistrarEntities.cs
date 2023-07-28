namespace Web_Roda_Llantas.Entities
{
    public class ProductosRegistrarEntities
    {
        public string Prod_Marca { get; set; } = string.Empty;
        public double Prod_Precio { get; set; }
        public int Prod_CantStock { get; set; }
        public int Prod_Proveedor_Id { get; set; }
        public int Prod_TipoProducto_Id { get; set; }
        public string Prod_Url_Img { get; set; }
    }
}
