using Message.Contracts;
using Movies.Api.DTOs;
using Movies.Api.Entity;
using Users.Api.Entities;
using UserType = Movies.Api.Enums.UserType;

namespace Movies.Api.Mapper;

public static class MovieMapper
{
    public static MovieDto ToDto(Movie movie)
    {
        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Genre = !string.IsNullOrWhiteSpace(movie.Genre) ? movie.Genre.Split(',') : [],
            ReleaseDate = movie.ReleaseDate,
            Rating = movie.Rating,
            MovieCollection = movie.MovieCollection
        };
    }
    
    public static MovieDto ToDto(Movie movie, Dictionary<UserType,string[]>? userDetails)
    {
        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Genre = !string.IsNullOrWhiteSpace(movie.Genre) ? movie.Genre.Split(',') : [],
            Directors = userDetails!.TryGetValue(UserType.Director, out var directors) ? directors : [],
            Actors = userDetails.TryGetValue(UserType.Actor, out var actors) ? actors : [],
            JuniorArtist = userDetails.TryGetValue(UserType.JuniorArtist, out var juniorArtists) ? juniorArtists : [],
            ReleaseDate = movie.ReleaseDate,
            Rating = movie.Rating,
            MovieCollection = movie.MovieCollection
        };
    }

    public static Movie ToEntity(MovieDto movieDto)
    {
        return new Movie
        {
            Id = movieDto.Id,
            Title = movieDto.Title,
            Genre = string.Join(",", movieDto.Genre),
            ReleaseDate = movieDto.ReleaseDate,
            Rating = movieDto.Rating,
            MovieCollection = movieDto.MovieCollection
        };
    }
    
    public static void ToEntity(MovieDto movieDto, Movie existingMovie)
    {
        existingMovie.Title = movieDto.Title;
        existingMovie.Genre = string.Join(",", movieDto.Genre);
        existingMovie.ReleaseDate = movieDto.ReleaseDate;
        existingMovie.Rating = movieDto.Rating;
        existingMovie.MovieCollection = movieDto.MovieCollection;
    }
}
