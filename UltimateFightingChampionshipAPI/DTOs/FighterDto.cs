using System.ComponentModel.DataAnnotations;

namespace UltimateFightingChampionshipAPI.DTOs
{
    public class FighterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }

    }
}
