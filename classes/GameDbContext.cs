using Microsoft.EntityFrameworkCore;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) {}

    public DbSet<Player> Players { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Response> Responses { get; set; }
    public DbSet<ScoreScale> ScoreScales { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Response>()
            .HasOne(r => r.Player)
            .WithMany()
            .HasForeignKey(r => r.PlayerId);

        modelBuilder.Entity<Response>()
            .HasOne(r => r.Event)
            .WithMany()
            .HasForeignKey(r => r.EventId);

        modelBuilder.Entity<ScoreScale>()
            .HasOne(s => s.Player)
            .WithMany()
            .HasForeignKey(s => s.PlayerId);
        
        modelBuilder.Entity<ChatMessage>()
            .HasOne(c => c.Player)
            .WithMany()
            .HasForeignKey(c => c.PlayerId);
        
    }
}