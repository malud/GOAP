using GOAP;
using NUnit.Framework;

namespace Tests.StateComaparerTests
{
	[TestFixture()]
	class when_Equals_called
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
			var result = new StateComaparer().Equals(source, destination);

			Assert.IsTrue(result);
		}

		private static bool result;
		private static State source;
		private static State destination;
	}
}
