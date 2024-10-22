using Microsoft.Extensions.DependencyInjection;
using System;

namespace BeyondNet.Ddd.Es.Test
{
    [TestClass]
    internal class TestMain
    {
        internal static ServiceProvider? serviceProvider;
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext _)
        {
            var serviceCollection = new ServiceCollection();

            //serviceCollection.AddTransient<IDependencyC, TransientDependencyC>();

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            serviceProvider?.Dispose();
        }
    }
}