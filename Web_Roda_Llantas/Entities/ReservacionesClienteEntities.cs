namespace Web_Roda_Llantas.Entities
{
	public class ReservacionesClienteEntities
	{
		public int? Res_Usuario_Id { get; set; }
		public List<string>? ServiciosReservados { get; set; }
		public List<string>? FechasServiciosReservados { get; set; }
		//Se usan para ver los detalles de la reservación y no solo los id
		public string? Veh_Placa { get; set; }
		public string Veh_Fecha_Lanzamiento { get; set; } = string.Empty;
		public string Veh_Marca { get; set; } = string.Empty;
		public string Veh_Modelo { get; set; } = string.Empty;

		//Se usan para los servicios de esta reservación
		public int? TotalReservados { get; set; }
		public DateTime? Ser_Fecha { get; set; }
		public string? Ser_Nombre { get; set; }
	}
}
