using Microsoft.EntityFrameworkCore;

namespace MassTransit_SQS.Extension;

public static class MigrationExtension
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        using var applicationContext = serviceScope.ServiceProvider.GetRequiredService<AppContext>();
        
        applicationContext.Database.Migrate();
    }
}