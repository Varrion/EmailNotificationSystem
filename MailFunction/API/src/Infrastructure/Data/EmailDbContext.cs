using API.Application.Interfaces;
using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Data;
public class EmailDbContext : DbContext, IEmailDbContext
{
    public DbSet<Client> Client { get; set; }
    public DbSet<Template> Template { get; set; }


    public EmailDbContext(DbContextOptions<EmailDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Template>()
            .HasKey(t => t.Id);
    }
}
