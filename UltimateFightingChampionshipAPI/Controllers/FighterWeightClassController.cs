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
    public class FighterWeightClassController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FighterWeightClassController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FighterWeightClass>>> Get()
        {
            if(_context.FighterWeightClasses == null)
            {
                return NoContent();
            }

            return await _context.FighterWeightClasses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FighterWeightClass>> GetById(int id)
        {
            if(_context.FighterWeightClasses == null)
            {
                return NoContent();
            }

            var fighterWeightClass = await _context.FighterWeightClasses.FirstOrDefaultAsync(fwc => fwc.Id == id);

            if(fighterWeightClass == null)
            {
                return NoContent();
            }

            return Ok(fighterWeightClass);
        }

        [HttpPost]
        public async Task<ActionResult<FighterWeightClass>> Post(FighterWeightClassDto fighterWeightClassDto)
        {
            if(fighterWeightClassDto == null)
            {
                return BadRequest("Invalid data!");
            }

            var fighter = _context.Fighters.FirstOrDefault(fighter => fighter.Id == fighterWeightClassDto.FighterId);
            var weightClass = _context.WeightClasses.FirstOrDefault(wc => wc.Id == fighterWeightClassDto.WeightClassId);

            if(fighter == null)
            {
                return BadRequest($"Fighter with the Id {fighter.Id} is Not Found! Try Again!");
            }
            
            if(weightClass == null)
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
    }
}
