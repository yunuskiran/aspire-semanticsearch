using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Event
{
    public int Id { get; init; }
    [StringLength(255)] public string? Name { get; init; }
    [StringLength(4000)] public string? Description { get; init; }
    [StringLength(512)] public string? Url { get; init; }
}