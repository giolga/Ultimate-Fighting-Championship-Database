using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using UltimateFightingChampionshipAPI.DTOs;
using UltimateFightingChampionshipAPI.Models;

namespace UltimateFightingChampionshipAPI.Controllers.Dapper
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightClassDapperController : ControllerBase
    {
        private readonly IConfiguration _config;

        public WeightClassDapperController(IConfiguration config)
        {
            this._config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeightClass>>> GetWeightClasses()
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            try
            {
                return (await connection.QueryAsync<WeightClass>("SELECT * FROM WeightClasses")).ToList();
            }
            catch
            {
                return BadRequest("Problem!");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeightClass>> GetWeightClass(int id)
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            var weightClass = await connection.QueryFirstOrDefaultAsync<WeightClass>("SELECT * FROM WeightClasses WHERE Id = @Id", new { Id = id });

            try
            {
                if (weightClass != null)
                {
                    return Ok(weightClass);
                }

                return NotFound($"Division with the id of {id} Not Found!");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<WeightClass>> PostWeightClass(WeightClassDto weightClassDto)
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            try
            {
                await connection.ExecuteAsync("INSERT INTO WeightClasses (ClassName) VALUES(@ClassName)", weightClassDto);
                return Ok(weightClassDto);
            }
            catch
            {
                return BadRequest("WeightClass insertion Failed! Try Again");
            }
        }

        [HttpPut]
        public async Task<ActionResult<WeightClass>> UpdateWeightClassDapper(int id, WeightClassDto weightClassDto)
        {
            if (weightClassDto == null)
            {
                return BadRequest("The Weight class field is required!");
            }
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            var getWeight = await connection.QueryFirstOrDefaultAsync<WeightClass>("SELECT * FROM WeightClasses WHERE Id = @Id", new { Id = id });

            if (getWeight is null)
            {
                return NotFound($"Weight class with the Id of {id} Not Found! Try Again!");
            }

            try
            {
                await connection.ExecuteAsync(@"UPDATE WeightClasses SET ClassName = @ClassName WHERE Id = @Id", new { Id = id, ClassName = weightClassDto.ClassName });
                return Ok(weightClassDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Update Failed! Error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<WeightClass>> DeleteWeightClassDapper(int id)
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));
            var getWeight = await connection.QueryFirstOrDefaultAsync<WeightClass>("SELECT * FROM WeightClasses WHERE Id = @Id", new { Id = id });

            if (getWeight == null)
            {
                return NotFound($"Weight class with the Id: {id} Not Found! Deletion Failed!");
            }

            try
            {
                await connection.ExecuteAsync("DELETE FROM WeightClasses WHERE Id = @Id", new {Id = id});
                return Ok("Deleted successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Deletion Failed! Error: {ex.Message}");
            }
        }

    }
}
