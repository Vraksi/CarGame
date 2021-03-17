using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class SqlContext : DbContext
    {
        public DbSet<Highscore> Highscores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = SamuraiAppData");
        }
    }
}