using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .SetIsOriginAllowed(_ => true);
    });
});
builder.Services.AddHostedService<ChatBackgroundService>();

var app = builder.Build();

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chathub");

app.Run();

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task SendImage(string user, string imageUrl)
    {
        await Clients.All.SendAsync("ReceiveImage", user, imageUrl);
    }
}

public class ChatBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Example: Periodically log or perform background work
        while (!stoppingToken.IsCancellationRequested)
        {
            // You can add logic here, e.g., clean up, broadcast, etc.
            await Task.Delay(10000, stoppingToken); // 10 seconds
        }
    }
}
