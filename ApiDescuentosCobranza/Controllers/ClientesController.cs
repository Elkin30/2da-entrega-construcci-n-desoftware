using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDescuentosCobranza.Data;
using ApiDescuentosCobranza.Models;

namespace ApiDescuentosCobranza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();

            return cliente;
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        // PUT: api/Clientes/5 (VERSIÓN MEJORADA)
        [HttpPut("{id}")]
        public async Task<ActionResult<Cliente>> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
                return BadRequest("El ID de la URL no coincide con el del cuerpo.");

            var existente = await _context.Clientes.FindAsync(id);

            if (existente == null)
                return NotFound();

            existente.Nombre = cliente.Nombre;
            existente.Correo = cliente.Correo;
            existente.Telefono = cliente.Telefono;
            existente.SaldoTotal = cliente.SaldoTotal;

            await _context.SaveChangesAsync();

            return Ok(existente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper
        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
