using GOAP;
using NUnit.Framework;

namespace Tests.StateComaparerTests
{
	[TestFixture()]
	class when_Distance_called_and_states_are_equal
	{
		[TestFixtureSetUp]
		public void Setup()
		{
        
			source = new State{
                    {"param1", 10},
                    {"param2", 20},
                    {"param3", 30},
                };
			destination = new State{
                    {"param3", 30},
                    {"param2", 20},
                    {"param1", 10},
                };
		}

		[Test]
		public void Test()
		{
			result = new StateComaparer().Distance(destination, source);
			Assert.That(result == 0.0);
		}

		private static double result;
		private static State source;
		private static State destination;
	}
}