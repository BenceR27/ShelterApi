using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelterApi.Models;
using ShelterApi.Models.DTOs;

namespace ShelterApi.Controllers
{
    [Route("animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalshelterContext _context;

        public AnimalsController(AnimalshelterContext context)
        {
            _context = context;
        }

        [HttpPost("newAnimal")]
        public async Task<IActionResult> NewAnimal([FromBody] NewAnimalDto newAnimalDto)
        {
            var shelter = await _context.Shelters.FirstOrDefaultAsync(s => s.Id == newAnimalDto.ShelterId);

            if (shelter is null)
            {
                return StatusCode(404, new { message = "Nincs ilyen menhely", result = (object?)null });
            }

            var animal = new Animal
            {
                Id = Guid.NewGuid(),
                Name = newAnimalDto.Name,
                Species = newAnimalDto.Species,
                Age = newAnimalDto.Age,
                Weight = newAnimalDto.Weight,
                RegTime = DateTime.Now,
                ShelterId = newAnimalDto.ShelterId
            };

            if (animal is not null)
            {
                await _context.Animals.AddAsync(animal);
                await _context.SaveChangesAsync();

                return StatusCode(201, new { message = "Felvéve", result = animal });
            }

            return StatusCode(400, new { message = "Hiba", result = animal });
        }

        [HttpGet("allAnimal")]
        public async Task<IActionResult> AllAnimal()
        {
            var animals = await _context.Animals.ToListAsync();

            if (animals is not null)
            {
                return StatusCode(200, new { message = "Lekérdezve", result = animals });
            }

            return StatusCode(400, new { message = "Hiba", result = animals });
        }

        [HttpGet("byId")]
        public async Task<IActionResult> AnimalById([FromQuery] Guid id)
        {
            var animal = await _context.Animals.SingleOrDefaultAsync(a => a.Id == id);

            if (animal is not null)
            {
                return StatusCode(200, new { message = "Lekérdezve", result = animal });
            }

            return StatusCode(404, new { message = "Nincs találat", result = animal });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAnimal([FromQuery] Guid id, [FromBody] UpdateAnimalDto updateAnimalDto)
        {
            var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);

            if (animal is not null)
            {
                var shelter = await _context.Shelters.FirstOrDefaultAsync(s => s.Id == updateAnimalDto.ShelterId);

                if (shelter is null)
                {
                    return StatusCode(404, new { message = "Nincs ilyen menhely", result = (object?)null });
                }

                animal.Name = updateAnimalDto.Name;
                animal.Species = updateAnimalDto.Species;
                animal.Age = updateAnimalDto.Age;
                animal.Weight = updateAnimalDto.Weight;
                animal.ShelterId = updateAnimalDto.ShelterId;

                _context.Animals.Update(animal);
                await _context.SaveChangesAsync();

                return StatusCode(200, new { message = "Frissítve", result = animal });
            }

            return StatusCode(404, new { message = "Nincs találat", result = animal });
        }

        [HttpDelete("deleteAnimal")]
        public async Task<IActionResult> DeleteAnimal([FromQuery] Guid id)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal is not null)
            {
                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();

                return StatusCode(200, new { message = "Törölve", result = animal });
            }

            return StatusCode(404, new { message = "Nincs találat", result = animal });
        }
    }
}
