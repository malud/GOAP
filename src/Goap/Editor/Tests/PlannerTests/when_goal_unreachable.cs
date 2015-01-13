using System.Collections.Generic;
using GOAP;
using GOAP.Planning;
using NUnit.Framework;

namespace Tests.PlannerTests
{
	[TestFixture()]
	class when_goal_unreachable : BaseContext
	{
		[TestFixtureSetUp]
		public void Setup()
		{
			planner = CreatePlanner();

			initialState = new State
            {
                {"1" , 3},
                {"2" , 6},
            };
			goalState = new State
            {
                {"1" , 5},
                {"2" , 5},
            };
		}

		[Test]
		public void Test()
		{
			plan = planner.MakePlan(initialState, goalState);

			Assert.IsEmpty(plan);
		}

		private static Planner<State> planner;
		private static State initialState;
		private static State goalState;
		private static IEnumerable<IPlanningAction<State>> plan;
	}
}