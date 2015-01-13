using GOAP;
using NUnit.Framework;

namespace Tests.StateComaparerTests
{
	[TestFixture()]
	class when_Distance_called
	{
		[TestFixtureSetUp]
		void Setup()
		{
			state1 = new State{
                    {"param1", 10},
                    {"param2", 20},
                    {"param3", 30},
                };
			state2 = new State{
                    {"param1", 10},
                    {"param2", 15},
                    {"param3", 30},
                };
			state3 = new State{
                    {"param1", 10},
                    {"param2", 20},
                    {"param3", 20},
                };
			state4 = new State{
                    {"param1", 10},
                    {"param2", 20},
                };
			state5 = new State{
                    {"param1", 10},
                    {"param4", 20},
                };
		}

		void Test()
		{
			distance12 = new StateComaparer().Distance(state1, state2);
			distance13 = new StateComaparer().Distance(state1, state3);
			distance14 = new StateComaparer().Distance(state1, state4);
			distance15 = new StateComaparer().Distance(state1, state5);
            
			Assert.Less(distance12, distance13);

			Assert.Less(distance12, distance14);

			Assert.Less(distance12, distance15);
		}
		private static double distance12;
		private static double distance13;
		private static double distance14;
		private static double distance15;
		private static State state1;
		private static State state2;
		private static State state3;
		private static State state4;
		private static State state5;
	}
}