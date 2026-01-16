using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracharSaarathi.Api.Data;
using PracharSaarathi.Api.Models;

namespace PracharSaarathi.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KaryakartaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KaryakartaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Karyakarta>>> GetKaryakartas()
        {
            return await _context.Karyakartas.OrderByDescending(k => k.CreatedAt).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Karyakarta>> GetKaryakarta(int id)
        {
            var karyakarta = await _context.Karyakartas.FindAsync(id);

            if (karyakarta == null)
            {
                return NotFound();
            }

            return karyakarta;
        }

        [HttpPost]
        public async Task<ActionResult<Karyakarta>> PostKaryakarta(Karyakarta karyakarta)
        {
            karyakarta.CreatedAt = DateTime.UtcNow;
            _context.Karyakartas.Add(karyakarta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKaryakarta", new { id = karyakarta.Id }, karyakarta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutKaryakarta(int id, Karyakarta karyakarta)
        {
            if (id != karyakarta.Id)
            {
                return BadRequest();
            }

            _context.Entry(karyakarta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KaryakartaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKaryakarta(int id)
        {
            var karyakarta = await _context.Karyakartas.FindAsync(id);
            if (karyakarta == null)
            {
                return NotFound();
            }

            _context.Karyakartas.Remove(karyakarta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KaryakartaExists(int id)
        {
            return _context.Karyakartas.Any(e => e.Id == id);
        }
    }
}
