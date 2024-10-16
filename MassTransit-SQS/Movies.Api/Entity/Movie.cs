using System.ComponentModel.DataAnnotations;
using Users.Api.Entities;

namespace Movies.Api.Entity;

public class Movie
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    
    public string Genre { get; set; } = string.Empty;
    
    public DateTime ReleaseDate { get; set; }

    public decimal Rating { get; set; }
    
    public decimal MovieCollection { get; set; }
    
    #region Navigation Properties

    public ICollection<UserMovieMapping> UserMovieMappings { get; set; } 

    #endregion
}
