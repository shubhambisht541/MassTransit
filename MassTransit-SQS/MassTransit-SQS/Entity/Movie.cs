using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MassTransit_SQS.Entity;

public class Movie
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    
    public string Genre { get; set; } = string.Empty;
    
    public string Directors { get; set; } = string.Empty;

    public string Actors { get; set; } = string.Empty;

    public string JuniorArtist { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }

    public decimal Rating { get; set; }
    
    public decimal MovieCollection { get; set; }
}
