using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        public DbSet<Sport> Sports { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Odd> Odds { get; set; }
        public DbSet<MatchType> MatchTypes { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sport>()
                .HasMany(s => s.Events)
                .WithOne(e => e.Sport);
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Sport)
                .WithMany(s => s.Events);
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Event)
                .WithMany(e => e.Matches);
            modelBuilder.Entity<Bet>()
                .HasOne(b => b.Match)
                .WithMany(m => m.Bets);
            modelBuilder.Entity<Odd>()
                .HasOne(o => o.Bet)
                .WithMany(b => b.Odds);
            modelBuilder.Entity<MatchType>()
                .HasMany(mt => mt.Matches)
                .WithOne(m => m.Type);
        }
    }
}
