using MassTransit;
using Message.Contracts;
using Microsoft.EntityFrameworkCore;
using Users.Api.Entities;
using UserType = Movies.Api.Enums.UserType;

namespace Movies.Api.Consumers;

public class UserMovieMappingConsumer : IConsumer<UpdateUserMovieMapping>
{
    private readonly AppContext _context;

    public UserMovieMappingConsumer(AppContext context)
    {
        _context = context;
    }
    public async Task Consume(ConsumeContext<UpdateUserMovieMapping> context)
    {
        try
        {
            foreach (var message in context.Message.UserDetails)
            {
                foreach (var userName in message.UserNames)
                {
                    var userInfo = await _context.User
                        .FirstOrDefaultAsync(user =>
                            user.Username.ToLower() == userName.ToLower()
                            && user.UserType == (UserType)message.UserType);

                    if (userInfo is null)
                    {
                        User user = new User { Username = userName, UserType = (UserType)message.UserType };
                        userInfo = _context.User.Add(user).Entity;
                    }

                    var userMovieMappingDetail = await _context.UserMovieMapping
                        .FirstOrDefaultAsync(userMovieMapping =>
                            userMovieMapping.UserId == userInfo.Id
                            && userMovieMapping.MovieId == context.Message.MovieId);

                    if (userMovieMappingDetail is not null) continue;

                    var userMovieMapping = new UserMovieMapping
                        { MovieId = context.Message.MovieId, UserId = userInfo.Id };
                    var mappingDetails = _context.UserMovieMapping.Add(userMovieMapping);
                    await _context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        await Task.CompletedTask;
    }
}