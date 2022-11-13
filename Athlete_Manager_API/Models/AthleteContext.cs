using Microsoft.EntityFrameworkCore;

namespace Athlete_Manager_API.Models
{
    public class AthleteContext : DbContext
    {
        public AthleteContext(DbContextOptions<AthleteContext> options) : base(options)
        {

        }

        public DbSet<Athlete> Athletes {get;set;} = null!;
    }
}