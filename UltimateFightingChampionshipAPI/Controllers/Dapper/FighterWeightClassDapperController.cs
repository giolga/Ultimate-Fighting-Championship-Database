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
        public async Task <ActionResult<IEnumerable<FighterWeightClass>>> GetAll()
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            try
            {
                return (await connection.QueryAsync<FighterWeightClass>("SELECT * FROM FighterWeightClasses")).ToList();
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<FighterWeightClass>> Post(FighterWeightClassDto fighterWeightClassDto)
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            var fighter = await connection.QueryFirstOrDefaultAsync<Fighter>("SELECT * FROM Fighters WHERE Id = @fighterId", new {fighterId = fighterWeightClassDto.FighterId});
            var weightClass = await connection.QueryFirstOrDefaultAsync<WeightClass>("SELECT * FROM WeightClasses WHERE Id = @weightClassId", new { weightClassId = fighterWeightClassDto.WeightClassId});

            if(fighter == null)
            {
                return BadRequest($"Fighter with the id {fighterWeightClassDto.FighterId} Not Found!");
            }
            
            if(weightClass == null)
            {
                return BadRequest($"Weight class with the id {fighterWeightClassDto.WeightClassId} Not Found!");
            }

            try
            {
                await connection.ExecuteAsync("INSERT INTO FighterWeightClasses (FighterId, WeightClassId) VALUES(@fighterId, @weightClassId)", new { fighterId = fighterWeightClassDto.FighterId, weightClassId = fighterWeightClassDto.WeightClassId});
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Unable to Post!");
            }
        }
    }
}
