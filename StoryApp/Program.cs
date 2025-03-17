using FluentValidation;
using Microsoft.OpenApi.Models;
using StoryApp.ActionFilters;
using StoryApp.Middleware;
using StoryApp.Service;
using StoryApp.Service.Abstract;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers(x =>
{
    x.Filters.Add<ValidatorActionFilter>();
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

builder.Services.AddSwaggerGen(
            o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo() { Title = "MyAPI", Version = "v1" });
            });

builder.Services.AddScoped<IStoryService, StoryService>();

builder.Services.AddHttpClient();  // ≈÷«›… HttpClient ≈·Ï DI
builder.Services.AddSingleton<VoiceRssTtsService>();  // ≈÷«›… VoiceRssTtsService


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<CultureInfoManager>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
