using MassTransit;
using Movies.Api.Consumers;

namespace Movies.Api.Configuration;

public static class MessageBrokerConfiguration
{
    public static void Build(WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<UserMovieMappingConsumer>().Endpoint(e => e.InstanceId = builder.Environment.EnvironmentName);
            busConfigurator.UsingAmazonSqs((context, config) =>
            {
                config.Host(builder.Configuration["AWSRegion"], hostConfigurator =>
                {
                    hostConfigurator.AccessKey(builder.Configuration["AmazonSQS:AccessKey"]);
                    hostConfigurator.SecretKey(builder.Configuration["AmazonSQS:SecretKey"]);
                    hostConfigurator.Scope("movies-platform", true);
                });
                config.ConfigureEndpoints(context);
            });
        });
    }
}