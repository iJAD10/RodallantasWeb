namespace Web_Roda_Llantas.Entities
{
    public class FacturaEntities
    {
        public int Fac_Id { get; set; }
        public DateTime Fac_Fecha { get; set; }
        public List<ProductosEntities> ListaProductos { get; set; } 
    }
}
