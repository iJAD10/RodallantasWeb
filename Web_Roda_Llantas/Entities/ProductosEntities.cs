namespace Web_Roda_Llantas.Entities
{
    public class ProductosEntities
    {
        public int Prod_Id { get; set; }
        public string Prod_Marca { get; set; } = string.Empty;
        public double Prod_Precio { get; set; }
        public double Prod_total { get; set; }
        public int Prod_CantStock { get; set; }
        public int Prod_Proveedor_Id { get; set; }
        public int Prod_TipoProducto_Id { get; set; }
        public int Prod_Cantidad { get; set; } = 0;
		public string Prod_Url_Img { get; set; }
	}
}
