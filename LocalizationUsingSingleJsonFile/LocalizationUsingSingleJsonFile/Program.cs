using LocalizationUsingSingleJsonFile;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Register the custom localization service
builder.Services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
// Configure localization options
// Configure default culture and supported cultures
var supportedCultures = new[] { "en-US", "es-ES", "fr-FR" }; // Add supported cultures as needed
var defaultCulture = new CultureInfo("en-US"); // Set your default culture here
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = supportedCultures.Select(culture => new CultureInfo(culture)).ToList(),
    SupportedUICultures = supportedCultures.Select(culture => new CultureInfo(culture)).ToList()
};

app.UseRequestLocalization(localizationOptions);


app.MapControllers();

app.Run();
