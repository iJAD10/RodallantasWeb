using Web_Roda_Llantas.Entities;

namespace Web_Roda_Llantas.Interfaces
{
    public interface IProveedoresModel
    {
        public int RegistrarProveedores(ProveedoresEntities entidad);
        public void ActualizarProveedores(ProveedoresEntities entidad);
        public List<ProveedoresEntities>? ConsultarProveedores();
        public ProveedoresEntities? ConsultarProveedor(long q);
        public int InactivarProveedor(ProveedoresEntities entidad);
        public List<ProveedoresEntities>? ConsultarProveedorXNombre();
    }
}
