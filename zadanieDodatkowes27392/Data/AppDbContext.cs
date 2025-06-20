using zadanieDodatkowes27392.Models;

namespace zadanieDodatkowes27392.Data;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Event> Events { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<EventSpeaker> EventSpeakers { get; set; }
    public DbSet<EventParticipant> EventParticipants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
