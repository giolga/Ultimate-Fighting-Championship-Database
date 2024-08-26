using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using UltimateFightingChampionshipAPI.DTOs;
using UltimateFightingChampionshipAPI.Models;

namespace UltimateFightingChampionshipAPI.Controllers.Dapper
{
    [Route("api/[controller]")]
    [ApiController]
    public class FighterWeightClassDapperController : ControllerBase
    {
        private readonly IConfiguration _config;
        public FighterWeightClassDapperController(IConfiguration config)
        {
            this._config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FighterWeightClass>>> GetAll()
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            try
            {
                return (await connection.QueryAsync<FighterWeightClass>("SELECT * FROM FighterWeightClasses")).ToList();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<FighterWeightClass>> Post(FighterWeightClassDto fighterWeightClassDto)
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            var fighter = await connection.QueryFirstOrDefaultAsync<Fighter>("SELECT * FROM Fighters WHERE Id = @fighterId", new { fighterId = fighterWeightClassDto.FighterId });
            var weightClass = await connection.QueryFirstOrDefaultAsync<WeightClass>("SELECT * FROM WeightClasses WHERE Id = @weightClassId", new { weightClassId = fighterWeightClassDto.WeightClassId });

            if (fighter == null)
            {
                return BadRequest($"Fighter with the id {fighterWeightClassDto.FighterId} Not Found!");
            }

            if (weightClass == null)
            {
                return BadRequest($"Weight class with the id {fighterWeightClassDto.WeightClassId} Not Found!");
            }

            try
            {
                await connection.ExecuteAsync(@"INSERT INTO FighterWeightClasses 
                                                (FighterId, WeightClassId)
                                                VALUES(@fighterId, @weightClassId)",
                                                new
                                                {
                                                    fighterId = fighterWeightClassDto.FighterId,
                                                    weightClassId = fighterWeightClassDto.WeightClassId
                                                });
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Unable to Post!");
            }
        }

        [HttpPut]
        public async Task<ActionResult<FighterWeightClass>> Update(int id, FighterWeightClassDto fighterWeightClassDto)
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            var fighterWeightClass = await connection.QueryFirstOrDefaultAsync("SELECT * FROM FighterWeightClasses WHERE Id = @Id", new { Id = id });

            if (fighterWeightClass == null)
            {
                return NotFound($"FighterWeightClass with the id of {id} Not Found! Try Again!");
            }

            try
            {
                await connection.ExecuteAsync(@"UPDATE FighterWEightClasses
                                                SET FighterId = @fighterId, WeightClassId = @weightClassId
                                                WHERE Id = @Id ",
                                                new
                                                {
                                                    Id = id,
                                                    fighterId = fighterWeightClassDto.FighterId,
                                                    weightClassId = fighterWeightClassDto.WeightClassId
                                                });

                return Ok("FighterWeightClass updated successfully!");
            }
            catch (Exception)
            {
                return BadRequest("Update Failed! Try Again!");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<FighterWeightClass>> Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            var fighterweightClass = await connection.QueryFirstOrDefaultAsync<FighterWeightClass>("SELECT * FROM FighterWeightClasses WHERE Id = @id", new { id = id });

            if (fighterweightClass == null)
            {
                return NotFound($"FighterWeightClass with the Id : {id} Not Found!");
            }

            try
            {
                await connection.ExecuteAsync("DELETE FROM FighterWeightClasses WHERE Id = @Id", new { Id = id });
                return Ok("Deleted Successfully");
            }
            catch (Exception)
            {
                return BadRequest($"FighterWeightClass Not Found! Invalid Id {id}");
            }
        }
    }
}
