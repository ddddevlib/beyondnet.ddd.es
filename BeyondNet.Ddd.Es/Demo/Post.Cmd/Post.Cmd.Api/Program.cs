using BeyondNet.Cqrs.Commands.Impl;
using BeyondNet.Cqrs.Commands.Interfaces;
using BeyondNet.Cqrs.Producers.Interfaces;
using BeyondNet.Ddd.Es.Impl;
using BeyondNet.Ddd.Es.Interfaces;
using BeyondNet.Ddd.Es.MongoDb;
using Confluent.Kafka;
using Post.Cmd.Api.CommandHandlers;
using Post.Cmd.Api.Commands;
using Post.Cmd.Domain.Aggregates;
using Post.Cmd.Infrastructure.Producers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
builder.Services.AddScoped<IEventStoreRepository, MongoDbEventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<AbstractCommandHandler<NewPostCommand>, NewPostCommandHandler>();
builder.Services.AddScoped<AbstractCommandHandler<EditMessageCommand>, EditMessageCommandHandler>();
builder.Services.AddScoped<AbstractCommandHandler<LikePostCommand>, LikePostCommandHandler>();
builder.Services.AddScoped<AbstractCommandHandler<DeletePostCommand>, DeletePostCommandHandler>();
builder.Services.AddScoped<AbstractCommandHandler<AddCommentCommand>, AddCommentCommandHandler>();
builder.Services.AddScoped<AbstractCommandHandler<RemoveCommentCommand>, RemoveCommentCommandHandler>();

builder.Services.AddScoped<IEventSourcingHandler<PostAggregate>, AbstractEventSourcingHandler<PostAggregate>>();

// register command handler methods
var dispatcher = new CommandDispatcher();
builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    dispatcher.RegisterHandler<NewPostCommand>(serviceProvider.GetRequiredService<AbstractCommandHandler<NewPostCommand>>().HandleAsync);
    dispatcher.RegisterHandler<EditMessageCommand>(serviceProvider.GetRequiredService<AbstractCommandHandler<EditMessageCommand>>().HandleAsync);
    dispatcher.RegisterHandler<LikePostCommand>(serviceProvider.GetRequiredService<AbstractCommandHandler<LikePostCommand>>().HandleAsync);
    dispatcher.RegisterHandler<AddCommentCommand>(serviceProvider.GetRequiredService<AbstractCommandHandler<AddCommentCommand>>().HandleAsync);
    dispatcher.RegisterHandler<RemoveCommentCommand>(serviceProvider.GetRequiredService<AbstractCommandHandler<RemoveCommentCommand>>().HandleAsync);
    dispatcher.RegisterHandler<DeletePostCommand>(serviceProvider.GetRequiredService<AbstractCommandHandler<DeletePostCommand>>().HandleAsync);
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
