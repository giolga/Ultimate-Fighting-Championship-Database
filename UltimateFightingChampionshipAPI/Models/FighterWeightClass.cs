using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UltimateFightingChampionshipAPI.Models
{
    public class FighterWeightClass
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("FighterFK")]
        public int FighterId { get; set; }  // Foreign key to Fighter
        [JsonIgnore]
        public Fighter FighterFK { get; set; } // Navigation property to Fighter

        [ForeignKey("WeightClassFK")]
        public int WeightClassId { get; set; }  // Foreign key to WeightClass
        [JsonIgnore]
        public WeightClass WeightClassFK { get; set; }  // Navigation property to WeightClass
    }
}
