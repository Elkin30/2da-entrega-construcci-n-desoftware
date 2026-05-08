using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDescuentosCobranza.Data;
using ApiDescuentosCobranza.Models;

namespace ApiDescuentosCobranza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampañaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CampañaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Campaña
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campaña>>> Get()
        {
            return await _context.Campañas.ToListAsync();
        }

        // GET: api/Campaña/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Campaña>> GetById(int id)
        {
            var campaña = await _context.Campañas.FindAsync(id);

            if (campaña == null)
                return NotFound();

            return campaña;
        }

        // POST: api/Campaña
        [HttpPost]
        public async Task<ActionResult<Campaña>> Post(Campaña campaña)
        {
            _context.Campañas.Add(campaña);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = campaña.Id }, campaña);
        }

        // PUT: api/Campaña/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Campaña campaña)
        {
            if (id != campaña.Id)
                return BadRequest("El ID no coincide.");

            var existente = await _context.Campañas.FindAsync(id);

            if (existente == null)
                return NotFound();

            existente.Nombre = campaña.Nombre;
            existente.PorcentajeDescuento = campaña.PorcentajeDescuento;
            existente.DiasAntesVencimiento = campaña.DiasAntesVencimiento;
            existente.TipoCliente = campaña.TipoCliente;
            existente.FechaInicio = campaña.FechaInicio;
            existente.FechaFin = campaña.FechaFin;
            existente.Activa = campaña.Activa;

            await _context.SaveChangesAsync();

            return Ok(existente);
        }

        // DELETE: api/Campaña/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var campaña = await _context.Campañas.FindAsync(id);

            if (campaña == null)
                return NotFound();

            _context.Campañas.Remove(campaña);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
