using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class EventDbContext(DbContextOptions<EventDbContext> options)
    : DbContext(options)
{
    public DbSet<Event> Events { get; set; }
}

