using ETrade.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETrade.Persistence.Extensions;

public static class MigrationDatabaseExtension
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        await using (var scope = app.Services.CreateAsyncScope())
        {
            await using (var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>())
            {
                if (dataContext != null)
                {
                    try
                    {
                        //await dataContext.Database.EnsureDeletedAsync();
                        await dataContext.Database.EnsureCreatedAsync();
                        //await dataContext.Database.MigrateAsync();
                    }
                    finally
                    {
                        await dataContext.DisposeAsync();
                    }
                }
            }
        }
    }
}