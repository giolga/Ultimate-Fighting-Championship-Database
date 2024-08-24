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
    public class WeightClassController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WeightClassController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeightClass>>> GetWeightClasses()
        {
            if (_context.WeightClasses == null)
            {
                return NotFound();
            }

            return await _context.WeightClasses.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<WeightClass>> PostWeightClass(WeightClassDto weightClassDto)
        {
            if (weightClassDto == null)
            {
                return NoContent();
            }

            var weightClass = new WeightClass()
            {
                ClassName = weightClassDto.ClassName
            };

            _context.WeightClasses.Add(weightClass);
            await _context.SaveChangesAsync();

            return Ok(weightClass);
        }
    }
}
