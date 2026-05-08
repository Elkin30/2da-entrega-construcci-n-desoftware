using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDescuentosCobranza.Data;
using ApiDescuentosCobranza.Models;

namespace ApiDescuentosCobranza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FacturasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Facturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            return await _context.Facturas.ToListAsync();
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null)
                return NotFound();

            return factura;
        }

        // POST: api/Facturas
        [HttpPost]
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFactura), new { id = factura.Id }, factura);
        }

        // PUT: api/Facturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(int id, Factura factura)
        {
            if (id != factura.Id)
                return BadRequest("El ID de la URL no coincide con el del cuerpo.");

            var existente = await _context.Facturas.FindAsync(id);

            if (existente == null)
                return NotFound();

            existente.Cliente = factura.Cliente;
            existente.Valor = factura.Valor;
            existente.FechaVencimiento = factura.FechaVencimiento;
            existente.Aplicada = factura.Aplicada;

            await _context.SaveChangesAsync();

            return Ok(existente);
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null)
                return NotFound();

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
