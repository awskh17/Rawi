using FluentValidation;
using Microsoft.OpenApi.Models;
using StoryApp.ActionFilters;
using StoryApp.Domain.Options;
using StoryApp.Middleware;
using StoryApp.Service;
using StoryApp.Service.Abstract;

var builder = WebApplication.CreateBuilder(args);

// ? Configure Kestrel **before** building the app
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);  // Set to listen on port 8080
});

// Add services to the container.
builder.Services.AddControllers(x =>
{
    x.Filters.Add<ValidatorActionFilter>();
});


builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo() { Title = "MyAPI", Version = "v1" });
});

builder.Services.AddHttpClient();
builder.Services.AddOptions<KeyOptions>()
    .BindConfiguration(KeyOptions.Key)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddScoped<IGeminiService, GeminiService>();
builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddScoped<TtsService>();

// ? CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// ? Middleware & Pipeline Configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

// ? Remove this because it's already configured above
//if (!builder.Environment.IsDevelopment())
//{
//    builder.WebHost.ConfigureKestrel(options =>
//    {
//        options.ListenAnyIP(8080);
//    });
//}

// ? Add Custom Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<CultureInfoManager>();

// ? Disable HTTPS redirection since Render handles it externally
// app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.MapControllers();
app.Run();
