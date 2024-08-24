using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UltimateFightingChampionshipAPI.Models
{
    public class WeightClass
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string ClassName { get; set; }

        [JsonIgnore]
        public ICollection<Fighter> Fighters { get; set; }
    }
}
