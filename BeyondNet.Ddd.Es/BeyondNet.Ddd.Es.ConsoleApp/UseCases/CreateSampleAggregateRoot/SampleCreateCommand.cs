using BeyondNet.Cqrs.Impl;
using BeyondNet.Cqrs.Interfaces;

namespace BeyondNet.Ddd.Es.ConsoleApp.UseCases.CreateSampleAggregateRoot
{
    public class SampleCreateCommand : ICommand<ResultSet>
    {
        public SampleCreateCommand(
            string name,
            string sampleEntityOneId,
            string sampleEntityOneName,
            string sampleEntityTwoId,
            string sampleEntityTwoName) {
            Name = name;
            SampleEntityOneId = sampleEntityOneId;
            SampleEntityOneName = sampleEntityOneName;
            SampleEntityTwoId = sampleEntityTwoId;
            SampleEntityTwoName = sampleEntityTwoName;
        }

        public string Name { get; }
        public string SampleEntityOneId { get; }
        public string SampleEntityOneName { get; }
        public string SampleEntityTwoId { get; }
        public string SampleEntityTwoName { get; }
    }
}
