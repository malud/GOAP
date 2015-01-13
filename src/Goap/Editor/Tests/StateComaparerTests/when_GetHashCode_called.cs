using GOAP;
using NUnit.Framework;

namespace Tests.StateComaparerTests
{
	[TestFixture()]
	class when_GetHashCode_called
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
			result = new StateComaparer().GetHashCode(source) == new StateComaparer().GetHashCode(destination);

			Assert.IsTrue(result);
		}

		private static bool result;
		private static State source;
		private static State destination;
	}
}
