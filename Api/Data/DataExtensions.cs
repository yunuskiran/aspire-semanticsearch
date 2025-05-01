using Api.Models;

namespace Api.Data;

public static class DataExtensions
{
    public static void InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<EventDbContext>();

        context.Events.AddRange(Events);

        context.SaveChanges();
    }

    private static IEnumerable<Event> Events =>
    [
        new Event
        {
            Id = 1,
            Name = "Tech Innovators Conference",
            Description = "Exploring advancements in AI, robotics, and quantum computing.",
            Url = "https://events.example.com/tech-innovators"
        },
        new Event
        {
            Id = 2,
            Name = "Startup Launch Summit",
            Description = "Showcasing new startups and their innovative products.",
            Url = "https://events.example.com/startup-launch"
        },
        new Event
        {
            Id = 3,
            Name = "Digital Marketing Expo",
            Description = "Latest trends and tools in the digital marketing space.",
            Url = "https://events.example.com/digital-marketing-expo"
        },
        new Event
        {
            Id = 4,
            Name = "Green Future Forum",
            Description = "Sustainable living and environmental tech innovations.",
            Url = "https://events.example.com/green-future"
        },
        new Event
        {
            Id = 5,
            Name = "HealthTech Global Summit",
            Description = "Technology revolutionizing healthcare and wellness.",
            Url = "https://events.example.com/healthtech-summit"
        },
        new Event
        {
            Id = 6,
            Name = "Creative Arts Fair",
            Description = "A celebration of local artists, designers, and performers.",
            Url = "https://events.example.com/creative-arts"
        },
        new Event
        {
            Id = 7,
            Name = "Blockchain & Crypto Meetup",
            Description = "Discussing blockchain trends and cryptocurrency adoption.",
            Url = "https://events.example.com/blockchain-meetup"
        },
        new Event
        {
            Id = 8,
            Name = "Women in Tech Conference",
            Description = "Empowering women in technology and leadership roles.",
            Url = "https://events.example.com/women-in-tech"
        },
        new Event
        {
            Id = 9,
            Name = "AI & Ethics Workshop",
            Description = "Discussing the ethical implications of artificial intelligence.",
            Url = "https://events.example.com/ai-ethics"
        },
        new Event
        {
            Id = 10,
            Name = "Future of Mobility Expo",
            Description = "Exploring electric vehicles, drones, and urban transport.",
            Url = "https://events.example.com/future-mobility"
        },
        new Event
        {
            Id = 11,
            Name = "Indie Game Dev Day",
            Description = "Networking and showcases for independent game developers.",
            Url = "https://events.example.com/indie-games"
        },
        new Event
        {
            Id = 12,
            Name = "SaaS Growth Bootcamp",
            Description = "Workshops for scaling SaaS businesses and retaining users.",
            Url = "https://events.example.com/saas-bootcamp"
        },
        new Event
        {
            Id = 13,
            Name = "Melbourne Coding Hackathon",
            Description = "24-hour coding challenge with tech prizes and recruitment.",
            Url = "https://events.example.com/melbourne-hackathon"
        },
        new Event
        {
            Id = 14,
            Name = "UX/UI Design Conference",
            Description = "User experience design trends, case studies, and tools.",
            Url = "https://events.example.com/ux-ui-conf"
        },
        new Event
        {
            Id = 15,
            Name = "Cybersecurity Awareness Day",
            Description = "Educating individuals and businesses on staying secure online.",
            Url = "https://events.example.com/cyber-awareness"
        },
        new Event
        {
            Id = 16,
            Name = "Finance & Fintech Forum",
            Description = "The future of banking, payments, and digital finance.",
            Url = "https://events.example.com/fintech-forum"
        },
        new Event
        {
            Id = 17,
            Name = "E-Commerce Leaders Summit",
            Description = "Discussing e-commerce strategies, logistics, and innovation.",
            Url = "https://events.example.com/ecommerce-leaders"
        },
        new Event
        {
            Id = 18,
            Name = "Remote Work World",
            Description = "Exploring productivity, tools, and culture for remote teams.",
            Url = "https://events.example.com/remote-world"
        },
        new Event
        {
            Id = 19,
            Name = "Cloud & DevOps Conference",
            Description = "Best practices in CI/CD, containerization, and cloud scaling.",
            Url = "https://events.example.com/devops-conf"
        },
        new Event
        {
            Id = 20,
            Name = "EdTech Innovation Festival",
            Description = "Showcasing education technology for schools and universities.",
            Url = "https://events.example.com/edtech-fest"
        }
    ];
}