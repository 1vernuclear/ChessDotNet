using Microsoft.EntityFrameworkCore;
using ChessAppSolution.Shared;

namespace ChessAppSolution.Data
{
    public class ChessDbContext : DbContext
    {
        public ChessDbContext(DbContextOptions<ChessDbContext> options) : base(options) { }  // NEW: Constructor for options injection

        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)  // Fallback for design-time/tools
            {
                optionsBuilder.UseSqlite("Data Source=chess.db");
            }
        }
    }
}