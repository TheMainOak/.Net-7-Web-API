using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Athlete_Manager_API.Models;

namespace Athlete_Manager_API.Controllers
{
    [Route("api/Athletes")]
    [ApiController]
    public class AthletesController : ControllerBase
    {
        private readonly AthleteContext _context;

        public AthletesController(AthleteContext context)
        {
            _context = context;
        }

        // GET: api/Athletes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AthleteDTO>>> GetAthletes()
        {
          if (_context.Athletes == null)
          {
              return NotFound();
          }
            return await _context.Athletes.Select(x => AthleteToDTO(x)).ToListAsync();
        }

        // GET: api/Athletes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AthleteDTO>> GetAthlete(long id)
        {
          if (_context.Athletes == null)
          {
              return NotFound();
          }
            var athlete = await _context.Athletes.FindAsync(id);

            if (athlete == null)
            {
                return NotFound();
            }

            return AthleteToDTO(athlete);
        }

        // PUT: api/Athletes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAthlete(long id, AthleteDTO athleteDTO)
        {
            if (id != athleteDTO.ID)
            {
                return BadRequest();
            }

            _context.Entry(athleteDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AthleteExists(id))
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

        // POST: api/Athletes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AthleteDTO>> PostAthlete(AthleteDTO athleteDTO)
        {
            var athlete = new Athlete{ID = athleteDTO.ID,
                FirstName = athleteDTO.FirstName,
                Surname = athleteDTO.Surname,
                Sport = athleteDTO.Sport,
                Age = athleteDTO.Age,
                IsActive = athleteDTO.IsActive};
            _context.Athletes.Add(athlete);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAthlete), new { id = athleteDTO.ID }, athleteDTO);
        }

        // DELETE: api/Athletes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAthlete(long id)
        {
            if (_context.Athletes == null)
            {
                return NotFound();
            }
            var athlete = await _context.Athletes.FindAsync(id);
            if (athlete == null)
            {
                return NotFound();
            }

            _context.Athletes.Remove(athlete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AthleteExists(long id)
        {
            return (_context.Athletes?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        private static AthleteDTO AthleteToDTO(Athlete athlete)
        {
            return new AthleteDTO
            {
                ID = athlete.ID,
                FirstName = athlete.FirstName,
                Surname = athlete.Surname,
                Sport = athlete.Sport,
                Age = athlete.Age,
                IsActive = athlete.IsActive
            };
        }
    }
}
