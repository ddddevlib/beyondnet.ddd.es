using BeyondNet.Cqrs.Impl;
using BeyondNet.Ddd.Es.Domain.Entities;
using BeyondNet.Ddd.Es.Interfaces;
using Microsoft.Extensions.Logging;

namespace BeyondNet.Ddd.Es.ConsoleApp.UseCases.CreateSampleAggregateRoot
{
    public class SampleCreateCommandHandler : AbstractCommandHandler<SampleCreateCommand, ResultSet>
    {
        private readonly IAggregateRootRepository _aggregateRootRepository;
        private readonly IEventStore<SampleAggregateRoot, SampleAggregateRootProps> _eventStore;

        public SampleCreateCommandHandler(IAggregateRootRepository aggregateRootRepository,
                                          IEventStore<SampleAggregateRoot, SampleAggregateRootProps> eventStore,
                                          ILogger<AbstractCommandHandler<SampleCreateCommand, ResultSet>> Logger) : base(Logger)
        {
            _aggregateRootRepository = aggregateRootRepository ?? throw new ArgumentNullException(nameof(aggregateRootRepository));
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        public override async Task<ResultSet> HandleCommand(SampleCreateCommand request, CancellationToken cancellationToken)
        {
            var aggregate = SampleAggregateRoot.Create(SampleName.Create("foo"),
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
