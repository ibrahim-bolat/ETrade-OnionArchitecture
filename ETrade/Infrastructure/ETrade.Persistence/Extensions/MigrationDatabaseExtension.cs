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
                        await dataContext.Database.MigrateAsync();
                    }
                    catch (Exception ex)
                    {
                        throw;
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