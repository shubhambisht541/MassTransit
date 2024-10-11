using MassTransit_SQS.DTOs;
using MassTransit_SQS.Entity;
using MassTransit_SQS.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MassTransit_SQS.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;

    private readonly AppContext _dbContext;

    public MovieController(ILogger<MovieController> logger, AppContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    [HttpGet(nameof(GetMovieDetails), Name = "GetMovieById")]
    public IActionResult GetMovieDetails([FromQuery] Guid id)
    {
        var movieDetail =  _dbContext.Movie.Find(id);

        if (movieDetail == null)
        {
            return NotFound(" Movie details does not exist");
        }
        
        return Ok(MovieMapper.ToDto(movieDetail));
    }

    [HttpPost(nameof(AddMovieDetails), Name = "AddMovie")]
    public IActionResult AddMovieDetails(MovieDto request)
    {
        try
        {
            var movieDetailEntity = MovieMapper.ToEntity(request);
            var movieDetail = _dbContext.Movie.Add(movieDetailEntity);
            _dbContext.SaveChanges();
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