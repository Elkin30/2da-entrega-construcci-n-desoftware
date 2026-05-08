namespace ApiDescuentosCobranza.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        // Foreign Key
        public int RolId { get; set; }

        // Navegación
        public Rol? Rol { get; set; }

        public Usuario()
        {
            FechaCreacion = DateTime.Now;
            Activo = true;
        }
    }
}
