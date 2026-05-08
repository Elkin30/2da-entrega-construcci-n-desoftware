using Microsoft.AspNetCore.Mvc;
using ApiDescuentosCobranza.Data;
using ApiDescuentosCobranza.Models;

namespace ApiDescuentosCobranza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DescuentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DescuentosController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Descuentos/calcular
        [HttpPost("calcular")]
        public IActionResult CalcularDescuento(int facturaId, int campañaId)
        {
            var factura = _context.Facturas.Find(facturaId);
            var campaña = _context.Campañas.Find(campañaId);

            if (factura == null)
                return NotFound("Factura no encontrada.");

            if (campaña == null)
                return NotFound("Campaña no encontrada.");

            // Evitar doble aplicación
            if (factura.Aplicada)
                return BadRequest("La factura ya tiene un descuento aplicado.");

            // Validar campaña activa
            if (!campaña.Activa)
                return BadRequest("La campaña está inactiva.");

            var hoy = DateTime.Now;

            // Validar vigencia
            if (hoy < campaña.FechaInicio || hoy > campaña.FechaFin)
                return BadRequest("La campaña no está vigente.");

            // Validar días antes vencimiento
            var diasRestantes = (factura.FechaVencimiento - hoy).Days;

            if (diasRestantes > campaña.DiasAntesVencimiento)
            {
                return BadRequest(
                    $"La factura no aplica. Faltan {diasRestantes} días para vencer.");
            }

            // Calcular descuento
            decimal descuento = factura.Valor *
                (campaña.PorcentajeDescuento / 100);

            // Calcular IVA
            decimal iva = descuento * 0.19m;

            // Total nota crédito
            decimal totalNotaCredito = descuento + iva;

            // Guardar descuento aplicado
            var descuentoAplicado = new DescuentoAplicado
            {
                FacturaId = factura.Id,
                CampañaId = campaña.Id,
                ValorDescuento = descuento,
                IVA = iva,
                TotalNotaCredito = totalNotaCredito,
                FechaAplicacion = DateTime.Now
            };

            _context.DescuentosAplicados.Add(descuentoAplicado);

            // Marcar factura aplicada
            factura.Aplicada = true;

            _context.SaveChanges();

            return Ok(new
            {
                Mensaje = "Descuento aplicado correctamente",
                DescuentoAplicadoId = descuentoAplicado.Id,
                FacturaId = factura.Id,
                Cliente = factura.Cliente,
                ValorFactura = factura.Valor,
                PorcentajeDescuento = campaña.PorcentajeDescuento,
                Descuento = descuento,
                IVA = iva,
                TotalNotaCredito = totalNotaCredito
            });
        }
    }
}
