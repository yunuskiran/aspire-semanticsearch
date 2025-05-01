using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;

namespace Api.Services;

public class EventAiService(
    EventDbContext dbContext,
    IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator,
    IVectorStoreRecordCollection<int, EventVector> collection)
{
    public async Task<IEnumerable<Event>> SemanticSearchAsync(string term)
    {
        var exist = await collection.CollectionExistsAsync();
        if (!exist)
            await InitializeAsync();

        var queryEmbedding = await embeddingGenerator.GenerateEmbeddingVectorAsync(term);

        var results =
            collection.SearchEmbeddingAsync(queryEmbedding, 3, new VectorSearchOptions<EventVector>
            {
                VectorProperty = input => input.Vector,
            });

        List<Event> events = [];
        await foreach (var resultItem in results)
        {
            events.Add(new Event()
            {
                Id = resultItem.Record.Id,
                Name = resultItem.Record.Name,
                Description = resultItem.Record.Description,
            });
        }

        return events;
    }

    private async Task InitializeAsync()
    {
        await collection.CreateCollectionIfNotExistsAsync();
        var data = await dbContext.Events.ToListAsync();
        foreach (var @event in data)
        {
            var info =
                $"[{@event.Name}] is a event and is described as [{@event.Description}]";

            var vector = new EventVector
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                Url = @event.Url,
                Vector = await embeddingGenerator.GenerateEmbeddingVectorAsync(info)
            };

            await collection.UpsertAsync(vector);
        }
    }
}