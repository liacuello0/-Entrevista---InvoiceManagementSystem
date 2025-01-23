using InvoiceManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleFacturasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DetalleFacturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var detallesFactura = await _context.DetallesFactura.Include(d => d.Factura).ToListAsync();
            return Ok(detallesFactura);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var detalleFactura = await _context.DetallesFactura
                .Include(d => d.Factura)
                .FirstOrDefaultAsync(m => m.DetalleFacturaId == id);
            if (detalleFactura == null)
            {
                return NotFound();
            }

            return Ok(detalleFactura);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DetalleFactura detalleFactura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.DetallesFactura.Add(detalleFactura);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = detalleFactura.DetalleFacturaId }, detalleFactura);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DetalleFactura detalleFactura)
        {
            if (id != detalleFactura.DetalleFacturaId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Entry(detalleFactura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleFacturaExists(id))
                {
                    return NotFound();
                }
                else throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var detalleFactura = await _context.DetallesFactura.FindAsync(id);
            if (detalleFactura == null)
            {
                return NotFound();
            }
            _context.DetallesFactura.Remove(detalleFactura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleFacturaExists(int id)
        {
            return _context.DetallesFactura.Any(e => e.DetalleFacturaId == id);
        }
    }
}
