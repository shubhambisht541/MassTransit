using MassTransit_SQS.Configuration;
using MassTransit_SQS.Extension;

var builder = WebApplication.CreateBuilder(args);

EndPointConfiguration.Build(builder);
DataProviderConfiguration.Build(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();