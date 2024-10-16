using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Movies.Api.Entity;

namespace Users.Api.Entities;

public class UserMovieMapping
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    [ForeignKey("Movie")]
    public Guid MovieId { get; set; }

    #region Navigation Properties

    public User User { get; set; }
    
    public Movie Movie { get; set; }

    #endregion
}