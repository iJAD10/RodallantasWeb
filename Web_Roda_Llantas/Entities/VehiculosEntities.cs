namespace Web_Roda_Llantas.Entities
{
    public class VehiculosEntities 
    {
        public int Veh_Id { get; set; }

        public string Veh_Fecha_Lanzamiento { get; set; } = string.Empty;

        public string Veh_Marca { get; set; } = string.Empty;

        public string Veh_Modelo { get; set; } = string.Empty;

        public string Veh_Placa { get; set; } = string.Empty;

        public int Veh_Tipo_Vehiculo_Id { get; set; }
    }
}
