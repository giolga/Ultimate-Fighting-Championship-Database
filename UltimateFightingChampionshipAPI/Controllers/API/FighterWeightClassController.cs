using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UltimateFightingChampionshipAPI.Data;
using UltimateFightingChampionshipAPI.DTOs;
using UltimateFightingChampionshipAPI.Models;

namespace UltimateFightingChampionshipAPI.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FighterWeightClassController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FighterWeightClassController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FighterWeightClass>>> Get()
        {
            if (_context.FighterWeightClasses == null)
            {
                return NoContent();
            }

            return await _context.FighterWeightClasses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FighterWeightClass>> GetById(int id)
        {
            if (_context.FighterWeightClasses == null)
            {
                return NoContent();
            }

            var fighterWeightClass = await _context.FighterWeightClasses
                .AsNoTracking().Include(f => f.FighterFK)
                .Include(wc => wc.WeightClassFK)
                .FirstOrDefaultAsync(fwc => fwc.Id == id);

            if (fighterWeightClass == null)
            {
                return NotFound($"FighterWeightClass with Id {id} not found.");
            }

            var dto = new FighterWeightClassDto
            {
                FighterId = fighterWeightClass.FighterId,
                WeightClassId = fighterWeightClass.WeightClassId,
                FighterName = $"{fighterWeightClass.FighterFK.FirstName} {fighterWeightClass.FighterFK.LastName}",
                WeightClassName = fighterWeightClass.WeightClassFK.ClassName
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<FighterWeightClass>> Post(FighterWeightClassDto fighterWeightClassDto)
        {
            if (fighterWeightClassDto == null)
            {
                return BadRequest("Invalid data!");
            }

            var fighter = _context.Fighters.FirstOrDefault(fighter => fighter.Id == fighterWeightClassDto.FighterId);
            var weightClass = _context.WeightClasses.FirstOrDefault(wc => wc.Id == fighterWeightClassDto.WeightClassId);

            if (fighter == null)
            {
                return BadRequest($"Fighter with the Id {fighter.Id} is Not Found! Try Again!");
            }

            if (weightClass == null)
            {
                return BadRequest($"Weight class with the Id {weightClass.Id} is Not Found! Try Again!");
            }

            FighterWeightClass fwc = new FighterWeightClass()
            {
                FighterId = fighterWeightClassDto.FighterId,
                WeightClassId = fighterWeightClassDto.WeightClassId
            };

            _context.FighterWeightClasses.Add(fwc);
            await _context.SaveChangesAsync();

            return Ok("Successfully added");
        }

        [HttpPut]
        public async Task<ActionResult<FighterWeightClass>> Put(int id, FighterWeightClassDto fighterWeightClassDto)
        {
            if (id != fighterWeightClassDto.FighterId)
            {
                return BadRequest("Fighter ID mismatch.");
            }

            var fighterWeight = await _context.FighterWeightClasses
                .Include(fwc => fwc.FighterFK)
                .Include(fwc => fwc.WeightClassFK)
                .FirstOrDefaultAsync(fwc => fwc.Id == id);

            if (fighterWeight == null)
            {
                return NotFound($"FighterWeightClass with Id {id} not found.");
            }

            var fighter = await _context.Fighters.FindAsync(fighterWeightClassDto.FighterId);
            var weightClass = await _context.WeightClasses.FindAsync(fighterWeightClassDto.WeightClassId);

            if (fighter == null)
            {
                return BadRequest($"Fighter with Id {fighterWeightClassDto.FighterId} not found.");
            }

            if (weightClass == null)
            {
                return BadRequest($"Weight class with Id {fighterWeightClassDto.WeightClassId} not found.");
            }

            fighterWeight.FighterId = fighterWeightClassDto.FighterId;
            fighterWeight.WeightClassId = fighterWeightClassDto.WeightClassId;

            _context.Entry(fighterWeight).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(fighterWeight);
        }

        [HttpDelete]
        public async Task<ActionResult<FighterWeightClass>> Delete(int id)
        {
            if (_context.FighterWeightClasses == null)
            {
                return NoContent();
            }

            var fighterWeightClass = await _context.FighterWeightClasses.FindAsync(id);

            if (fighterWeightClass == null)
            {
                return NotFound($"No Fighter Weight Class exists with the Id of {id}");
            }

            _context.FighterWeightClasses.Remove(fighterWeightClass);
            await _context.SaveChangesAsync();

            return Ok($"Deleted successfully!");
        }
    }
}
