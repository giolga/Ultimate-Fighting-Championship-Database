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
    public class FightersDapperController : ControllerBase
    {
        private readonly IConfiguration _config;

        public FightersDapperController(IConfiguration config)
        {
            this._config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fighter>>> GetFighters()
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            var fighters = await connection.QueryAsync<Fighter>("SELECT * FROM Fighters");
            return Ok(fighters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fighter>> GetFighter(int id)
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));
            var fighter = await connection.QueryFirstOrDefaultAsync("SELECT * FROM Fighters WHERE Id = @id", new { id = id });

            if (fighter == null)
            {
                return NotFound($"Fighter with the id {id} Not Found!");
            }

            return Ok(fighter);
        }

        [HttpPost]
        public async Task<ActionResult<Fighter>> PostFighter(FighterDto fighterDto)
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            try
            {
                await connection.ExecuteAsync("INSERT INTO Fighters (FirstName, LastName, Nickname, DateOfBirth, Nationality) VALUES (@FirstName, @LastName, @Nickname, @DateOfBirth, @Nationality)", fighterDto);
            }
            catch
            {
                return BadRequest("Query problem!");
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Fighter>> UpdateFighter(int id, FighterDto fighter)
        {
            if (fighter == null)
            {
                return BadRequest("The fighter field is required.");
            }

            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            var getFighter = await connection.QueryFirstOrDefaultAsync("SELECT * FROM Fighters WHERE Id = @id", new { id = id });

            if (getFighter == null)
            {
                return NotFound($"Fighter with the Id: {id} Not Found!");
            }

            try
            {
                await connection.ExecuteAsync(@"UPDATE Fighters 
                                                SET FirstName = @FirstName, LastName = @LastName, Nickname = @Nickname, 
                                                DateOfBirth = @DateOfBirth, Nationality = @Nationality 
                                                WHERE Id = @Id",
                                                new
                                                {
                                                    FirstName = fighter.FirstName,
                                                    LastName = fighter.LastName,
                                                    Nickname = fighter.Nickname,
                                                    DateOfBirth = fighter.DateOfBirth,
                                                    Nationality = fighter.Nationality,
                                                    Id = id
                                                });

                return Ok("Fighter updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Update Failed! Error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Fighter>> DeleteFighter(int id)
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            var getFighter = await connection.QueryFirstOrDefaultAsync("SELECT * FROM Fighters WHERE Id = @id", new { id = id });

            if (getFighter == null)
            {
                return NotFound($"Fighter with the Id: {id} Not Found! Deletion Failed!");
            }

            try
            {
                await connection.ExecuteAsync("DELETE FROM Fighters WHERE Id = @id", new { id = id });
                return Ok("Fighter deleted successfully!");
            }
            catch
            {
                return BadRequest("Delete Failed!");
            }
        }

    }
}
