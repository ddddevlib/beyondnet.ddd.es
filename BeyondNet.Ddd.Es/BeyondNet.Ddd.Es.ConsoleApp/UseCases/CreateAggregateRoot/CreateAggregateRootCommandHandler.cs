using BeyondNet.Cqrs.Impl;
using BeyondNet.Ddd.Es.ConsoleApp.Entities;
using BeyondNet.Ddd.Es.Interfaces;
using Microsoft.Extensions.Logging;

namespace BeyondNet.Ddd.Es.ConsoleApp.UseCases.CreateSampleAggregateRoot
{
    public class CreateAggregateRootCommandHandler : AbstractCommandHandler<CreateAggregateRootCommand, ResultSet>
    {
        private readonly IAggregateRootRepository _aggregateRootRepository;
        private readonly IEventStore<SampleAggregateRoot> _eventStore;

        public CreateAggregateRootCommandHandler(IAggregateRootRepository aggregateRootRepository,
                                          IEventStore<SampleAggregateRoot> eventStore,
                                          ILogger<AbstractCommandHandler<CreateAggregateRootCommand, ResultSet>> Logger) : base(Logger)
        {
            _aggregateRootRepository = aggregateRootRepository ?? throw new ArgumentNullException(nameof(aggregateRootRepository));
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        public override async Task<ResultSet> HandleCommand(CreateAggregateRootCommand request, CancellationToken cancellationToken)
        {
            var aggregate = SampleAggregateRoot.Create(SampleName.Create("foo foo"),
                                                SampleEntity.Load(request.SampleEntityOneId, new SampleEntityProps(SampleName.Create(request.SampleEntityOneName))),
                                                SampleEntity.Load(request.SampleEntityTwoId, new SampleEntityProps(SampleName.Create(request.SampleEntityTwoName))));

            if (!aggregate.IsValid())
            {
                return ResultSet.Error($"Errors is aggregate found: {aggregate.GetBrokenRules.GetBrokenRulesAsString()}");
            }
            await _eventStore.Save(aggregate);

            await _aggregateRootRepository.Add(aggregate);

            return ResultSet.Success();
        }
    }
}
