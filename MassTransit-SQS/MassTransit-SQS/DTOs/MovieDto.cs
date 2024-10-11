namespace MassTransit_SQS.DTOs;

public class MovieDto
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string[] Genre { get; set; } = [];

    public string[] Directors { get; set; } = [];

    public string[] Actors { get; set; } = [];

    public string[] JuniorArtist { get; set; } = [];

    public DateTime ReleaseDate { get; set; }

    public decimal Rating { get; set; }
    
    public decimal MovieCollection { get; set; }
}