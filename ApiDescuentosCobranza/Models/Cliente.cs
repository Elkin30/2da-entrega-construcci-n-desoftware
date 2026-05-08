using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiDescuentosCobranza.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Telefono { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "El correo no tiene formato válido")]
        public string Correo { get; set; } = string.Empty;

        public decimal SaldoTotal { get; set; }

        public ICollection<Factura>? Facturas { get; set; }
    }
}
