using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDescuentosCobranza.Models
{
    public class Factura
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [Required(ErrorMessage = "El valor de la factura es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El valor debe ser mayor o igual a 0")]
        public decimal Valor { get; set; }

        // Fecha en la que se crea la factura
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Fecha límite de pago
        public DateTime FechaVencimiento { get; set; }

        // Indica si la factura ya fue usada en algún proceso (descuento/pago/etc.)
        public bool Aplicada { get; set; }
    }
}
