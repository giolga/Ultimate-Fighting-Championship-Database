using Microsoft.EntityFrameworkCore;
using UltimateFightingChampionshipAPI.Models;

namespace UltimateFightingChampionshipAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Fighter> Fighters { get; set; }
        public DbSet<WeightClass> WeightClasses {  get; set; }

    }
}
