using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace API.Infrastructure.Data;
public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<EmailServiceDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        //await initialiser.SeedAsync();
    }

    public class EmailServiceDbContextInitialiser
    {
        private readonly ILogger<EmailServiceDbContextInitialiser> _logger;
        private readonly EmailServiceDbContext _context;

        public EmailServiceDbContextInitialiser(ILogger<EmailServiceDbContextInitialiser> logger, EmailServiceDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        //public async Task SeedAsync()
        //{
        //    try
        //    {
        //        await TrySeedAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while seeding the database.");
        //        throw;
        //    }
        //}

        //public async Task TrySeedAsync()
        //{
        //    // Default data
        //    // Seed, if necessary
        //    if (!_context.Clients.Any())
        //    {
        //        _context.Templates.Add(new Template
        //        {
        //            Name = "Todo List",
        //            Items =
        //        {
        //            new TodoItem { Title = "Make a todo list 📃" },
        //            new TodoItem { Title = "Check off the first item ✅" },
        //            new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
        //            new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
        //        }
        //        });

        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
