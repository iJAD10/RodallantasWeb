namespace Web_Roda_Llantas.Entities
{
    public class CarritoListarEntities
    {
        public int IdCarrito { get; set; }
        public int Prod_Id { get; set; }
        public string Marca { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal TotalPorProducto { get; set; }
    }
}
