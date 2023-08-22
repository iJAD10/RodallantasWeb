namespace Web_Roda_Llantas.Entities
{
    public class OrdenCompraEntities
    {
        public int Orden_Id { get; set; }
        public int Usu_Id { get; set; }
        public DateTime FechaOrden { get; set; }
        public double Total { get; set; }
        public bool Estado { get; set; }
    }
}
