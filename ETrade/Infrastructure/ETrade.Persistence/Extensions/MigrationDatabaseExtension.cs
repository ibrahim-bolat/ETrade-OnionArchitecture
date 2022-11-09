using ETrade.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETrade.Persistence.Extensions;

public static class MigrationDatabaseExtension
{
    public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
    {
        await using (var scope = app.Services.CreateAsyncScope())
        {
            await using (var dataContext = scope.ServiceProvider.GetService<DataContext>())
            {
                if (dataContext != null)
                {
                    try
                    {
                        //await dataContext.Database.EnsureDeletedAsync();
                        await dataContext.Database.EnsureCreatedAsync();
                        await dataContext.Database.MigrateAsync();
                    }
                    finally
                    {
                        await dataContext.DisposeAsync();
                    }
                }
            }
        }
        return app;
    }
}