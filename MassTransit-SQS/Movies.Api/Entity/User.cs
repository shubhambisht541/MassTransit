using System.ComponentModel.DataAnnotations;
using Movies.Api.Enums;

namespace Users.Api.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    public string Username { get; set; } = string.Empty;
    
    public UserType UserType { get; set; }
    
    public string PhotoUrl { get; set; } = string.Empty;

    #region Navigation Properties

    public ICollection<UserMovieMapping> UserMovieMappings { get; set; } 

    #endregion
}