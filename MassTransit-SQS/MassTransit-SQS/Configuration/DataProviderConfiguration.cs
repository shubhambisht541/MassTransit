
using Microsoft.EntityFrameworkCore;
namespace MassTransit_SQS.Configuration;
public class DataProviderConfiguration
{ 
    public static void Build(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppContext>(c =>
        {
            c.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"), 
            sqlServerOptionsAction : sqlOptions => 
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10, 
                    maxRetryDelay: TimeSpan.FromSeconds(5), 
                    errorNumbersToAdd: null);
            });
        });
    }
}