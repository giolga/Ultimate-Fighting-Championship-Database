using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UltimateFightingChampionshipAPI.Data;
using UltimateFightingChampionshipAPI.DTOs;
using UltimateFightingChampionshipAPI.Models;

namespace UltimateFightingChampionshipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FightersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FightersController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fighter>>> GetFighters()
        {
            if (_context.Fighters == null)
            {
                return NotFound();
            }

            var fighters = await _context.Fighters.ToListAsync();  // Use ToListAsync to get all fighters

            return Ok(fighters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fighter>> GetFighter(int id)
        {
            if (_context.Fighters == null)
            {
                return NotFound();
            }

            var fighter = _context.Fighters.FirstOrDefault(f => f.Id == id);

            if (fighter == null)
            {
                return NotFound();
            }

            return Ok(fighter);
        }

        [HttpPost]
        public async Task<ActionResult<FighterDto>> PostFighter(FighterDto fighterDto)
        {
            if (fighterDto == null)
            {
                return NoContent();
            }

            var fighter = new Fighter()
            {
                FirstName = fighterDto.FirstName,
                LastName = fighterDto.LastName,
                Nickname = fighterDto.Nickname,
                DateOfBirth = fighterDto.DateOfBirth,
                Nationality = fighterDto.Nationality,
            };

            _context.Fighters.Add(fighter);
            await _context.SaveChangesAsync();

            return Ok("Fighter successfully added");
        }

        [HttpPut]
        public async Task<ActionResult<Fighter>> Updatefighter(int id, FighterDto fighter)
        {
            if (_context.Fighters == null)
            {
                return NoContent();
            }

            var person = _context.Fighters.FirstOrDefault(f => f.Id == id);
            if (person == null)
            {
                return NoContent();
            }

            person.FirstName = fighter.FirstName;
            person.LastName = fighter.LastName;
            person.Nickname = fighter.Nickname;
            person.DateOfBirth = fighter.DateOfBirth;
            person.Nationality = fighter.Nationality;

            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(fighter);
        }

        [HttpDelete]
        public async Task<ActionResult<Fighter>> DeleteFighter(int id)
        {
            var fighter = await _context.Fighters.FirstOrDefaultAsync(f => f.Id == id);
        
            if (fighter == null)
            {
                return NotFound();
            }

            _context.Fighters.Remove(fighter);
            await _context.SaveChangesAsync();
            return Ok("Fighter remove successfully!");
        }
    }
}
