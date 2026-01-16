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
    public class BirthdayController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BirthdayController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("today")]
        public async Task<ActionResult<IEnumerable<Karyakarta>>> GetTodaysBirthdays()
        {
            var today = DateTime.Today;
            // Filter by Month and Day
            var birthdays = await _context.Karyakartas
                .Where(k => k.DateOfBirth.Month == today.Month && k.DateOfBirth.Day == today.Day)
                .ToListAsync();

            return birthdays;
        }
    }
}
