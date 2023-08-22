namespace Web_Roda_Llantas.Entities
{
    public class OrdenCompraListar
    {
        public int Orden_Id { get; set; }
        public string Usu_Nombre { get; set; }
        public DateTime FechaOrden { get; set; }
        public double Total { get; set; }
        public bool Estado { get; set; }
    }
}
