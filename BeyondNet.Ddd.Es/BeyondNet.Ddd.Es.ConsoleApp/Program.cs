// See https://aka.ms/new-console-template for more information
using BeyondNet.Ddd;
using BeyondNet.Ddd.Es.Domain.Entities;

Console.WriteLine("Start Application");

Console.WriteLine("1. Creating a SampleEntity");

var sampleEntity1 = SampleEntity.Create(SampleName.Create("foo"));

Console.WriteLine("2. Creating another SampleEntity");

var sampleEntity2 = SampleEntity.Create(SampleName.Create("foo"));

Console.WriteLine("3. Creating an AggregateRoot linking Samples Entities");

var aggregateRoot = SampleAggregateRoot.Create(SampleName.Create("foo"), sampleEntity1, sampleEntity2);



