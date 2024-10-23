
Console.WriteLine("Setup Dependencies");

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddFilter("Microsoft", LogLevel.Warning)
        .AddFilter("System", LogLevel.Warning)
        .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
        .AddConsole();
});
ILogger logger = loggerFactory.CreateLogger<Program>();

var services = new ServiceCollection();

services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

services.AddDbContext<EsDbContext>(options =>
{
    options.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
});

services.AddLogging(cfg =>
{
    cfg.AddConsole();
});

services.AddScoped<IAggregateRootRepository, AggregateRootRepository>();
services.AddScoped<IEventStore<SampleAggregateRoot>, EventStore<SampleAggregateRoot>>();
services.AddScoped<IEventStoreRepository, EventStoreRepository>();

var serviceProvider = services.BuildServiceProvider();

var mediator = serviceProvider.GetRequiredService<IMediator>();


Console.WriteLine("1. Creating a Command");

var command = new CreateAggregateRootCommand("Sample Aggregate Root", Guid.NewGuid().ToString(), "Sample Entity One",Guid.NewGuid().ToString(),"Sample Entity Two");

Console.WriteLine("2. Creating a Command Handler");

var result  =  await mediator.Send(command);

if (result.IsSuccess)
{
    Console.WriteLine("Command executed successfully");
}
else
{
    Console.WriteLine("Command failed");
}