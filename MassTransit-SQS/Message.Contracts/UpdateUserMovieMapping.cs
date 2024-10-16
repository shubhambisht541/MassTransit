namespace Message.Contracts;

public record UpdateUserMovieMapping
{
    public Guid MovieId { get; set; }
    
    public UserDetails[] UserDetails { get; set; }
}

public record UserDetails
{
    public string[] UserNames { get; init; }
    
    public UserType UserType { get; init; }
}

public enum UserType
{
    Actor,
    Director,
    JuniorArtist
}