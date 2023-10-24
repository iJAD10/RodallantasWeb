using Web_Roda_Llantas.Entities;

namespace Web_Roda_Llantas.Interfaces
{
    public interface ICompras
    {
        public void FinalizarCompra(int usuId, int total,string correoUsuario);
        public IEnumerable<OrdenCompraListar> Listar();
        public IEnumerable<ListarDetalleOrdenPorId> ListarDetalleOrdenPorId(int orden_Id);
        public int CambiarCompletado(int orden_Id);
    }
}
