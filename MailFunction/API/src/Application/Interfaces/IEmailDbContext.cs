using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Interfaces;
public interface IEmailDbContext
{
    DbSet<Client> Client { get; }
    DbSet<Template> Template { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
