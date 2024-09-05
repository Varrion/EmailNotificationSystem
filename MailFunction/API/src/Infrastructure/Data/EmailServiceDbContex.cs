using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Data;
public class EmailServiceDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Template> Templates { get; set; }

    public EmailServiceDbContext(DbContextOptions<EmailServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure entity properties and relationships here if needed
        modelBuilder.Entity<Client>()
            .HasKey(c => c.Id); // Primary key for Client

        modelBuilder.Entity<Template>()
            .HasKey(t => t.Id); // Primary key for Template
    }
}
