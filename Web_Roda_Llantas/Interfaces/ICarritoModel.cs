using Web_Roda_Llantas.Entities;

namespace Web_Roda_Llantas.Interfaces
{
    public interface ICarritoModel
    {
        public bool RegistrarCompra(List<ProductosEntities> entidad);
        IEnumerable<CarritoListarEntities> ObtenerCarritoPorUsuario(int usuId);
       

    }
}
