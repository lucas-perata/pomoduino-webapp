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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddBlazorBootstrap();

builder.Services.AddScoped<TimeEntryService>();
builder.Services.AddScoped<ClientTimeEntryService>();    
builder.Services.AddScoped<ITimeEntryService, TimeEntryService>();  
builder.Services.AddScoped<ITimeEntryService, ClientTimeEntryService>(); 

builder.Services.AddHttpClient<ClientTimeEntryService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7241/"); // Cambia a tu URL base
});

builder.Services.AddHttpClient<ClientTimeEntryService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:7241/"); // Cambia a tu URL base
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

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCors("AllowAllOrigins");

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Agregado de controles para crear API 
app.MapControllers();

app.Run();
