using api.Models;
using api.Repository;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
    DotNetEnv.Env.Load(); 
#endif

builder.Services.AddControllers();

builder.Services.AddScoped<IAdoptionFormRepository, AdoptionFormRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IFavoritedPetRepository, FavoritedPetRepository>();
builder.Services.AddScoped<IPetsRepository, PetsRepository>();
builder.Services.AddScoped<IShelterRepository, SheltersRepository>();
builder.Services.AddScoped<IUserAccountsRepository, UserAccountsRepository>();

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

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("OpenPolicy");

app.MapControllers();


app.Run();
