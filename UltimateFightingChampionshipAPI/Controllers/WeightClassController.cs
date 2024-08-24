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

        [HttpPut]
        public async Task<ActionResult<WeightClass>> UpdateWeightClass(int id, WeightClassDto weightDto)
        {
            if (weightDto == null)
            {
                return NoContent();
            }

            var weightClass = await _context.WeightClasses.FirstOrDefaultAsync(w => w.Id == id);

            if (weightClass == null)
            {
                return NotFound();
            }

            weightClass.ClassName = weightDto.ClassName;

            _context.Entry(weightClass).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(weightClass);
        }

        [HttpDelete]
        public async Task<ActionResult<WeightClass>> DeleteWeightClass(int id)
        {
            var weightClass = await _context.WeightClasses.FirstOrDefaultAsync(w => w.Id == id);

            if(weightClass == null)
            {
                return NotFound();
            }

            _context.WeightClasses.Remove(weightClass);
            await _context.SaveChangesAsync();

            return Ok("Weight class removed successfully!");
        }
    }
}
