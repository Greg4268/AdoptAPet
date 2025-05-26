using api.Models;
using api.Repository;

Console.WriteLine("=== STARTING APPLICATION ===");

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
    DotNetEnv.Env.Load(); 
    Console.WriteLine("Loaded .env file");
#endif

Console.WriteLine("Adding controllers...");
builder.Services.AddControllers();

Console.WriteLine("Adding repositories...");
// Comment out ALL repositories temporarily to test
/*
builder.Services.AddScoped<IAdoptionFormRepository, AdoptionFormRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IFavoritedPetRepository, FavoritedPetRepository>();
builder.Services.AddScoped<IPetsRepository, PetsRepository>();
builder.Services.AddScoped<IShelterRepository, SheltersRepository>();
builder.Services.AddScoped<IUserAccountsRepository, UserAccountsRepository>();
*/

Console.WriteLine("Adding CORS...");
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

Console.WriteLine("Adding Swagger...");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine("Building app...");
var app = builder.Build();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

Console.WriteLine("Configuring pipeline...");
app.UseAuthorization();
app.UseCors("OpenPolicy");
app.MapControllers();

Console.WriteLine($"Starting on port {port}...");
app.Run();