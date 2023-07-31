using Web_Roda_Llantas.Entities;

namespace Web_Roda_Llantas.Interfaces
{
	public interface IReservacionesModel
	{
		public int RegistrarReservacion(ReservacionesEntities entidad);
		public List<ReservacionesEntities>? ConsultarReservaciones();
		public List<ReservacionesEntities>? DetallesReservacion(long q);
		public int CambiarCompletado(ReservacionesEntities entidad);
        public int RegistrarVehiculoYReservacion(ReservacionesEntities entidad);
    }
}
