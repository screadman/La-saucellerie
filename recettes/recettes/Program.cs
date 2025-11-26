using Microsoft.EntityFrameworkCore;
using recettes.Models;
using NLog.Web;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Host.UseNLog();

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5, //Limite de 5 requêtes
                Window = TimeSpan.FromSeconds(10),
                QueueLimit = 0, // Optionnel : pas de file d'attente
                AutoReplenishment = true // Permet de recharger les quotas automatiquement
            });
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests; // Définit le code de retour 429
    options.OnRejected = async (context, token) =>
    {
            string message = "Vous avez atteint le nombre maximal des demandes autorisées. Un maximum de 5 requêtes par 10 secondes est autorisé.";

            context.HttpContext.Response.ContentType = "text/plain";
            await context.HttpContext.Response.WriteAsync(message, token); // Retourne le message lorsque la limite est atteinte
    };
});

builder.Services.AddControllers();
builder.Services.AddDbContext<RecettesContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("CONNEXION_BD")
    ));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseRateLimiter();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();