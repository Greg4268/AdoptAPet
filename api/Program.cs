using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// load environment variables 
Env.Load();

var dbConfig = new {
    Host = Env.GetString("DB_HOST"),
    Port = Env.GetString("DB_PORT"),
    User = Env.GetString("DB_USER"),
    Password = Env.GetString("DB_PASS"),
    Name = Env.GetString("DB_NAME")
};

Console.WriteLine($"Connecting to {dbConfig.Host}:{dbConfig.Port} as {dbConfig.User}");

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("OpenPolicy");

app.MapControllers();

app.Run();
