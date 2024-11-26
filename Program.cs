using Microsoft.EntityFrameworkCore;
using TimerApp.Components;
using TimerApp.Data;
using TimerApp.Services;
using Microsoft.OpenApi.Models; // Add this using directive
using Microsoft.Extensions.DependencyInjection; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));

builder.Services.AddScoped<TimeEntryService>();
builder.Services.AddScoped<ClientTimeEntryService>();    
builder.Services.AddScoped<ITimeEntryService, TimeEntryService>();  
builder.Services.AddScoped<ITimeEntryService, ClientTimeEntryService>(); 

builder.Services.AddHttpClient<ClientTimeEntryService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7241/"); // Cambia a tu URL base
});

builder.Services.AddHttpClient<ITimeEntryService, ClientTimeEntryService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7241/"); // Cambia a tu URL base
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Agregado de controles para crear API 
app.MapControllers();

app.Run();
