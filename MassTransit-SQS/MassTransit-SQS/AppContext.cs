using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;
using MassTransit_SQS.Entity;
using Microsoft.EntityFrameworkCore;

namespace MassTransit_SQS;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
            
    }

    public DbSet<Movie> Movie { get; set; }
}
