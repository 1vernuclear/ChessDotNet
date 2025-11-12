using ChessAppSolution.Client.Pages;
using ChessAppSolution.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// NEW: Add MVC controller services - this enables API endpoints in controllers for things like multiplayer or chess logic.
builder.Services.AddControllers();

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

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ChessAppSolution.Client._Imports).Assembly);

app.Run();