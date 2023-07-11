namespace Web_Roda_Llantas.Entities
{
    public class ProveedoresEntities
    {
        public int Prov_Id { get; set; }
        public string Prov_Apellido_Agente { get; set; } = string.Empty;
        public string Prov_Nombre_Agente { get; set; } = string.Empty;
        public string Prov_Correo { get; set; } = string.Empty;
        public string Prov_Direccion { get; set; } = string.Empty;
        public string Prov_Telefono { get; set; } = string.Empty;
        public string Prov_Telefono_Fijo { get; set; } = string.Empty;
        public string Prov_Nombre_Empresa { get; set; } = string.Empty;
        public bool Prov_Estado { get; set; }
    }
}
