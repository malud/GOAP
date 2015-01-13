using System.Collections.Generic;
using System.Linq;
using GOAP;
using GOAP.Planning;
using NUnit.Framework;

namespace Tests.PlannerTests
{
	[TestFixture()]
	public class when_goal_reachable : BaseContext
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
                {"2" , 4},
            };
		}

		[Test]
		public void Test()
		{
			plan = planner.MakePlan(initialState, goalState);

			Assert.IsNotEmpty(plan);

			Assert.That(plan.Count() == 2);
		}
		private static Planner<State> planner;
		private static State initialState;
		private static State goalState;
		private static IEnumerable<IPlanningAction<State>> plan;
	}
}
