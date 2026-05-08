using System;
using System.ComponentModel.DataAnnotations;

namespace ApiDescuentosCobranza.Models
{
    public class Campaña
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Required]
        [Range(1, 100)]
        public decimal PorcentajeDescuento { get; set; }

        [Required]
        [Range(1, 365)]
        public int DiasAntesVencimiento { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoCliente { get; set; } = null!;

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        public bool Activa { get; set; } = true;
    }
}