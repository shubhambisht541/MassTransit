using Movies.Api.Entity;
using Microsoft.EntityFrameworkCore;
using Users.Api.Entities;

namespace Movies.Api;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
            
    }

    public DbSet<Movie> Movie { get; set; }
    
    public DbSet<User> User { get; set; }
    
    public DbSet<UserMovieMapping> UserMovieMapping { get; set; }
}
