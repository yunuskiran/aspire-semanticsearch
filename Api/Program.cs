using Api.Data;
using Api.Models;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddDbContext<EventDbContext>(options =>
    options.UseInMemoryDatabase("TestDb"));

builder.Services.AddScoped<EventAiService>();

builder.AddOllamaApiClient("ollama-all-minilm").AddEmbeddingGenerator();

builder.Services.AddInMemoryVectorStoreRecordCollection<int, EventVector>("events");

var app = builder.Build();
app.MapDefaultEndpoints();
app.InitializeDatabase();
app.MapGet("/search/{term}", async (string term, EventAiService service) =>
    {
        var response = await service.SemanticSearchAsync(term);
        return Results.Ok(response);
    }).WithName("Search")
    .Produces<IEnumerable<Event>>(statusCode: StatusCodes.Status200OK);

app.UseHttpsRedirection();

app.Run();