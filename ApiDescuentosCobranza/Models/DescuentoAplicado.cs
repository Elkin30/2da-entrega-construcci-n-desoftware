using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDescuentosCobranza.Models
{
    public class DescuentoAplicado
    {
        public int Id { get; set; }

        [Required]
        public int FacturaId { get; set; }

        [ForeignKey("FacturaId")]
        public Factura? Factura { get; set; }

        [Required]
        public int CampañaId { get; set; }

        [ForeignKey("CampañaId")]
        public Campaña? Campaña { get; set; }

        [Required]
        public decimal ValorDescuento { get; set; }

        [Required]
        public decimal IVA { get; set; }

        [Required]
        public decimal TotalNotaCredito { get; set; }

        public DateTime FechaAplicacion { get; set; } = DateTime.Now;
    }
}
