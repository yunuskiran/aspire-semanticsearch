using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.VectorData;

namespace Api.Models;

public class EventVector
{
    [VectorStoreRecordKey] public int Id { get; set; }
    [VectorStoreRecordData] public string? Name { get; set; }
    [VectorStoreRecordData] public string? Description { get; set; }
    [VectorStoreRecordData] public string? Url { get; set; }

    [NotMapped]
    [VectorStoreRecordVector(500, DistanceFunction = DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float> Vector { get; init; }
}