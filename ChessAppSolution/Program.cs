using ChessAppSolution.Client.Pages;
using ChessAppSolution.Components;
using ChessAppSolution.Hubs;  // Add this using for ChessHub namespace
using ChessAppSolution.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// NEW: Add MVC controller services - this enables API endpoints in controllers for things like multiplayer or chess logic.
builder.Services.AddControllers();

// Add SignalR services
builder.Services.AddSignalR();

builder.Services.AddDbContext<ChessDbContext>(options => options.UseSqlite("Data Source=chess.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseStaticFiles();  // Updated: UseStaticFiles() is the modern equivalent for serving static assets; MapStaticAssets() might be from an older template.

// NEW: Map controller routes - this activates any controllers we add (e.g., in Controllers folder).
app.MapControllers();

// Map the hub (moved earlier for better pipeline order)
app.MapHub<ChessHub>("/chessHub");

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ChessAppSolution.Client._Imports).Assembly);

app.Run();

