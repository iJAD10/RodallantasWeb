namespace Web_Roda_Llantas.Entities
{
	public class ReservacionesEntities
	{
        public int Res_Id { get; set; }
        public bool? Res_Completada { get; set; }
        public string? Res_Estado { get; set; } = string.Empty;
        public int? Res_Usuario_Id { get; set; }
        public List<string>? ServiciosReservados { get; set; }
        public List<string>? FechasServiciosReservados { get; set; }
        public string Veh_Id { get; set; }
        //Se usa para el envío de correos
        public string? Usuario_Correo { get; set; }
        //Se usan para ver los detalles de la reservación y no solo los id
        public string? Usu_Cedula { get; set; }
        public string? Usu_Nombre { get; set; }
        public string? Veh_Placa { get; set; }
        public string Veh_Fecha_Lanzamiento { get; set; } = string.Empty;
        public string Veh_Marca { get; set; } = string.Empty;
        public string Veh_Modelo { get; set; } = string.Empty;
        public int? TotalReservados { get; set; }
        //Se usan para los servicios de esta reservación
        public DateTime? Ser_Fecha { get; set; }
        public string? Ser_Nombre { get; set; }
    }
}
