var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder
    .AddOllama("ollama", 11434)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithOpenWebUI();

var embed = ollama.AddModel("all-minilm");

var api = builder
    .AddProject<Projects.Api>("api")
    .WithReference(embed)
    .WaitFor(embed);


builder.Build().Run();