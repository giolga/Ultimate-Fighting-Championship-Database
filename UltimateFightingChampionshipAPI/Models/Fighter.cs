using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UltimateFightingChampionshipAPI.Models
{
    public class Fighter
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string Nickname { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required, MaxLength(50)]
        public string Nationality { get; set; }

        [JsonIgnore]
        public ICollection<WeightClass> WeightClasses { get; set; }

    }
}
