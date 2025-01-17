namespace Movies.Api.Configuration;

public static class EndPointConfiguration
{
    public static void Build(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
}