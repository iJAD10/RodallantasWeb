namespace Web_Roda_Llantas.Entities
{
    public class UsuarioEntities
    {
        public int Usu_Id { get; set; }
        public string Usu_Cedula { get; set; } = string.Empty;
        public string Usu_Nombre { get; set; } = string.Empty;
        public string Usu_Correo { get; set; } = string.Empty;
        public string Usu_Clave { get; set; } = string.Empty;
        public bool Usu_Estado { get; set; }
        public string Token { get; set; } = string.Empty;
		public int UR_Rol_Id { get; set; }
        public int Usu_Num_Carrito { get; set; }

    }
}
