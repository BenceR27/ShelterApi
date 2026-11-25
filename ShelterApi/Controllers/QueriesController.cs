using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelterApi.Models;

namespace ShelterApi.Controllers
{
    [Route("queries")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly AnimalshelterContext _context;

        public QueriesController(AnimalshelterContext context)
        {
            _context = context;
        }

        [HttpGet("allSheltersWithAnimals")]
        public async Task<IActionResult> AllSheltersWithAnimals()
        {
            var shelters = await _context.Shelters
                .Include(s => s.Animals)
                .ToListAsync();

            if (shelters is not null)
            {
                return StatusCode(200, new { message = "Lekérdezve", result = shelters });
            }

            return StatusCode(400, new { message = "Hiba", result = shelters });
        }

        [HttpGet("animalsOfShelter")]
        public async Task<IActionResult> AnimalsOfShelter([FromQuery] Guid shelterId)
        {
            var shelter = await _context.Shelters
                .Include(s => s.Animals)
                .Where(s => s.Id == shelterId)
                .FirstOrDefaultAsync();

            if (shelter is not null)
            {
                return StatusCode(200, new { message = "Lekérdezve", result = shelter });
            }

            return StatusCode(404, new { message = "Nincs találat", result = shelter });
        }
    }
}
