using InvoiceManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FacturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFacturas()
        {
            var facturas = await _context.Facturas.Include(f => f.Cliente).ToListAsync();
            return Ok(facturas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFactura(int id)
        {
            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFactura([FromBody] Factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFactura), new { id = factura.FacturaId }, factura);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFactura(int id, [FromBody] Factura factura)
        {
            if (id != factura.FacturaId)
            {
                return BadRequest();
            }
            _context.Entry(factura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.FacturaId == id);
        }
    }
}
