using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDescuentosCobranza.Data;

namespace ApiDescuentosCobranza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DescuentosAplicadosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DescuentosAplicadosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DescuentosAplicados
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var historial = await (
                from d in _context.DescuentosAplicados
                join f in _context.Facturas
                    on d.FacturaId equals f.Id
                join c in _context.Campañas
                    on d.CampañaId equals c.Id
                select new
                {
                    d.Id,
                    Cliente = f.Cliente,
                    ValorFactura = f.Valor,
                    Campaña = c.Nombre,
                    d.ValorDescuento,
                    d.IVA,
                    d.TotalNotaCredito,
                    d.FechaAplicacion
                }
            ).ToListAsync();

            return Ok(historial);
        }

        // GET: api/DescuentosAplicados/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var descuento = await (
                from d in _context.DescuentosAplicados
                join f in _context.Facturas
                    on d.FacturaId equals f.Id
                join c in _context.Campañas
                    on d.CampañaId equals c.Id
                where d.Id == id
                select new
                {
                    d.Id,
                    Cliente = f.Cliente,
                    ValorFactura = f.Valor,
                    Campaña = c.Nombre,
                    d.ValorDescuento,
                    d.IVA,
                    d.TotalNotaCredito,
                    d.FechaAplicacion
                }
            ).FirstOrDefaultAsync();

            if (descuento == null)
                return NotFound();

            return Ok(descuento);
        }
    }
}
