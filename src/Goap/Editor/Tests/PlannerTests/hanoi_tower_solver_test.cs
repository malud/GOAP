using System.Collections.Generic;
using System.Linq;
using GOAP;
using GOAP.Planning;
using NUnit.Framework;

namespace Tests.PlannerTests
{
	[TestFixture()]
	public class hanoi_tower_solver_test
	{
		//A1 1  B1 |  C1 |
		//A2 2  B2 |  C2 |
		//A3 3  B3 |  C3 |
		//A4 4  B4 |  C4 |
		//A5 5  B5 |  C5 |
		//A6 6  B6 |  C6 |
		//A7 7  B7 |  C7 |
		//  / \   / \   / \

		[TestFixtureSetUp]
		public void Setup()
		{
			initialState = new State
            {
                {"A1", 1},
                {"A2", 2},
                {"A3", 3},
                {"A4", 4},
                {"A5", 5},
                {"A6", 6},
                {"A7", 7},
            };
			goalState = new State
            {
                {"C1", 1},
                {"C2", 2},
                {"C3", 3},
                {"C4", 4},
                {"C5", 5},
                {"C6", 6},
                {"C7", 7},
            };
			planningActions = new List<PlanningAction<State>>
            {
                new PlanningAction<State>(
                    name: "Move from A to B",
                    validator: state => Validate(state, "A", "B"),
                    executor: state => Move(state, "A", "B")),
                new PlanningAction<State>(
                    name: "Move from A to C",
                    validator: state => Validate(state, "A", "C"),
                    executor: state => Move(state, "A", "C")),
                new PlanningAction<State>(
                    name: "Move from B to A",
                    validator: state => Validate(state, "B", "A"),
                    executor: state => Move(state, "B", "A")),
                new PlanningAction<State>(
                    name: "Move from B to C",
                    validator: state => Validate(state, "B", "C"),
                    executor: state => Move(state, "B", "C")),
                new PlanningAction<State>(
                    name: "Move from C to A",
                    validator: state => Validate(state, "C", "A"),
                    executor: state => Move(state, "C", "A")),
                new PlanningAction<State>(
                    name: "Move from C to B",
                    validator: state => Validate(state, "C", "B"),
                    executor: state => Move(state, "C", "B")),
            };
		}

		[Test]
		public void Test()
		{
			var comparer = new StateComaparer() as IPlanningStateComparer<State>;

			planner = new Planner<State>(PlanningMethod.DepthFirst, planningActions.ToArray(), comparer);

			plan = planner.MakePlan(initialState, goalState);

			Assert.IsNotEmpty(plan);

			Assert.That(plan.Count() == 127);
		}


		private static List<PlanningAction<State>> planningActions;
		private static Planner<State> planner;
		private static IEnumerable<IPlanningAction<State>> plan;
		private static State initialState;
		private static State goalState;

		private static bool Validate(State x, string srcName, string dstName)
		{
			var srcTop = 0;
			var srcTrigger = false;
			var dstTop = 0;
			var dstTrigger = false;

			for (var i = 1; i < 8; i++)
			{
				if (!srcTrigger)
					srcTrigger = x.TryGetValue(srcName + i, out srcTop);
				if (!dstTrigger)
					dstTrigger = x.TryGetValue(dstName + i, out dstTop);
			}

			return srcTrigger && (!dstTrigger || dstTop > srcTop);
		}

		private static void Move(State state, string srcName, string dstName)
		{
			int srcPosition = state
                .Where(kv => kv.Key.StartsWith(srcName))
				.Select(kv => kv.Key.Remove(0, 1))
                .Select(s => int.Parse(s))
                .Min();

			List<int> dstPosition = state
				.Where(kv => kv.Key.StartsWith(dstName))
				.Select(kv => kv.Key.Remove(0, 1))
                .Select(s => int.Parse(s))
                .ToList();

			var a = srcName + srcPosition;
			var dstNormalized = (dstPosition.Count == 0) ? 7 : dstPosition.Min() - 1;
			string b = (dstNormalized == 0) ? "" : dstName + dstNormalized;

			state.Add(b, state [a]);
			state.Remove(a);
		}
	}
}