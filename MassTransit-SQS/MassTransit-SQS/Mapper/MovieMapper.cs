using MassTransit_SQS.DTOs;
using MassTransit_SQS.Entity;

namespace MassTransit_SQS.Mapper;

public static class MovieMapper
{
    public static MovieDto ToDto(Movie movie)
    {
        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Genre = !string.IsNullOrWhiteSpace(movie.Genre) ? movie.Genre.Split(',') : [],
            Directors = !string.IsNullOrWhiteSpace(movie.Directors) ? movie.Directors.Split(',') : [],
            Actors = !string.IsNullOrWhiteSpace(movie.Actors) ? movie.Actors.Split(',') : [],
            JuniorArtist = !string.IsNullOrWhiteSpace(movie.JuniorArtist) ? movie.JuniorArtist.Split(',') : [],
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
            Directors = string.Join(",", movieDto.Directors),
            Actors = string.Join(",", movieDto.Actors),
            JuniorArtist = string.Join(",", movieDto.JuniorArtist),
            ReleaseDate = movieDto.ReleaseDate,
            Rating = movieDto.Rating,
            MovieCollection = movieDto.MovieCollection
        };
    }
    
    public static void ToEntity(MovieDto movieDto, Movie existingMovie)
    {
        existingMovie.Title = movieDto.Title;
        existingMovie.Genre = string.Join(",", movieDto.Genre);
        existingMovie.Directors = string.Join(",", movieDto.Directors);
        existingMovie.Actors = string.Join(",", movieDto.Actors);
        existingMovie.JuniorArtist = string.Join(",", movieDto.JuniorArtist);
        existingMovie.ReleaseDate = movieDto.ReleaseDate;
        existingMovie.Rating = movieDto.Rating;
        existingMovie.MovieCollection = movieDto.MovieCollection;
    }
}
