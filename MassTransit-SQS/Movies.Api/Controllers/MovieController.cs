using MassTransit;
using Message.Contracts;
using Movies.Api.DTOs;
using Movies.Api.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Movies.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;

    private readonly AppContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public MovieController(ILogger<MovieController> logger, AppContext dbContext, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
    }
    
    [HttpGet(nameof(GetMovieDetails), Name = "GetMovieById")]
    public IActionResult GetMovieDetails([FromQuery] Guid id)
    {
        var movieDetail =  _dbContext.Movie
            .Include(m => m.UserMovieMappings)
            .ThenInclude(u => u.User)
            .FirstOrDefault(item => item.Id == id);

        if (movieDetail == null)
        {
            return NotFound(" Movie details does not exist");
        }
        
        var userDetails = movieDetail.UserMovieMappings
            .GroupBy(item => item.User.UserType)
            .ToDictionary(
                item => item.Key,
                item => item.Select(u => u.User.Username).ToArray());
        
        return Ok(MovieMapper.ToDto(movieDetail, userDetails));
    }

    [HttpPost(nameof(AddMovieDetails), Name = "AddMovie")]
    public async Task<IActionResult> AddMovieDetails(MovieDto request)
    {
        try
        {
            var movieDetailEntity = MovieMapper.ToEntity(request);
            var movieDetail = _dbContext.Movie.Add(movieDetailEntity);
            _dbContext.SaveChanges();
            
            await _publishEndpoint.Publish(new UpdateUserMovieMapping
            {
                MovieId = movieDetailEntity.Id,
                UserDetails =
                [
                    new UserDetails { UserNames = request.Actors, UserType = UserType.Actor },
                    new UserDetails { UserNames = request.Directors, UserType = UserType.Director },
                    new UserDetails { UserNames = request.JuniorArtist, UserType = UserType.JuniorArtist }
                ]
            });
            
            return CreatedAtRoute("GetMovieById", new { id = movieDetailEntity.Id }, movieDetailEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut(nameof(UpdateMovieDetails), Name = "UpdateMovie")]
    public IActionResult UpdateMovieDetails([FromQuery] Guid id, MovieDto request)
    {
        var movieDetail =  _dbContext.Movie.Find(id);

        if (movieDetail == null)
        {
            return NotFound("Movie details does not exist");
        }

        MovieMapper.ToEntity(request, movieDetail);
        _dbContext.SaveChanges();
        return Ok(new { movieDetail.Id, Message = "Movie details updated successfully "});
    }
    
    
    [HttpDelete(nameof(DeleteMovieDetails), Name = "DeleteMovie")]
    public IActionResult DeleteMovieDetails([FromQuery] Guid id)
    {
        var movieDetail =  _dbContext.Movie.Find(id);

        if (movieDetail == null)
        {
            return NotFound(" Movie details does not exist");
        }

        _dbContext.Movie.Remove(movieDetail);
        _dbContext.SaveChanges();
        return Ok(new { movieDetail.Id, Message = "Movie details deleted successfully "});
    }
}