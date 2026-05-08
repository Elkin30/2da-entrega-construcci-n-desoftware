using System.Collections.Generic;

namespace ApiDescuentosCobranza.Models
{
    public class Rol
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        // Navegación inversa
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
