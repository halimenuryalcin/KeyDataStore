/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
*/

using Marten;
using MartenProject;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL bağlantı dizesi
var connectionString = "Host=postresqlWithmarten;Port=5432;Database=marten_db;Username=postgres;Password=password";

// Marten DocumentStore'u ekleyin
builder.Services.AddSingleton<IDocumentStore>(DocumentStore.For(options =>
{
    options.Connection(connectionString);
    options.Schema.For<DataRecord>().Identity(x => x.Key); // Primary key olarak "Key" tanımlanıyor
}));

var app = builder.Build();
app.MapPost("/data", async (DataRecord record, IDocumentStore store) =>
{
    try
    {
        Console.WriteLine("gelen kayıt"+record);
        using var session = store.LightweightSession();
        session.Store(record);
        await session.SaveChangesAsync();
        return Results.Ok("Data saved/updated successfully.");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error saving data: {ex.Message}");
    }
});


app.MapGet("/data/{key}", async (string key, IDocumentStore store) =>
{
    try
    {
        using var session = store.LightweightSession();
        var record = await session.Query<DataRecord>().FirstOrDefaultAsync(x => x.Key == key);
        return record is not null ? Results.Json(record) : Results.NotFound($"Record with key '{key}' not found.");
    }
    catch (Exception ex)
    {
        return Results.Problem($"hata hata hata Error fetching data: {ex.Message}");
    }
});

app.MapDelete("/data/{key}", async (string key, IDocumentStore store) =>
{
    using var session = store.LightweightSession();
    var record = await session.Query<DataRecord>().FirstOrDefaultAsync(x => x.Key == key);
    
    if (record is not null)
    {
        session.Delete(record);  // Kaydı sil
        await session.SaveChangesAsync();
        return Results.Ok("Data deleted successfully.");
    }

    return Results.NotFound("Record not found.");
});


app.Run();

