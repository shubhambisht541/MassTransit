using Microsoft.EntityFrameworkCore;

namespace Movies.Api.Extension;

public static class MigrationExtension
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        try
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            using var applicationContext = serviceScope.ServiceProvider.GetRequiredService<AppContext>();

            applicationContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}