using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Roda_Llantas.Controllers;
using Web_Roda_Llantas.Entities;

namespace Web_Roda_Llantas.Interfaces
{
    public interface IUsuarioModel
    {
        public UsuarioEntities? ValidarCredenciales(UsuarioEntities entidad);
        public List<UsuarioEntities>? ConsultarUsuarios();
        public int InactivarUsuario(UsuarioEntities entidad);
        public void ActualizarUsuario(UsuarioEntities entidad);
        public void RecuperarContrasenna(UsuarioEntities entidad);
        public UsuarioEntities? ConsultarUsuario(long q);
        public int RegistrarUsuario(UsuarioEntities entidad);
        public string? BuscarExisteCorreo(string Usu_Correo);
    }
}
