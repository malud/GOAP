using System.Collections.Generic;
using System.Linq;
using GOAP.Graph;
using GOAP.Planning;
using GOAP.PrioritizedCollections;

namespace GOAP
{
	public class Planner<T> : IPlanner<T>
	{
		private readonly PlanningMethod planningMethod;
		private readonly IEnumerable<IPlanningAction<T>> planningActions;
		private readonly IPlanningStateComparer<T> planningStateComparer;

		public Planner(PlanningMethod planningMethod, IEnumerable<IPlanningAction<T>> planningActions, IPlanningStateComparer<T> planningStateComparer)
		{
			this.planningMethod = planningMethod;
			this.planningActions = planningActions;
			this.planningStateComparer = planningStateComparer;
		}

		public IEnumerable<IPlanningAction<T>> MakePlan(T initialState, T goalState)
		{
			var visitedStates = new HashSet<T>(planningStateComparer) { initialState };
			var unvisitedPathes = UnvisitedPathes<Path<IPlanningAction<T>>>();

			var initialPossibleActions = planningActions
                .Where(action => action.CanExecute(initialState))
                .Select(action => new Path<IPlanningAction<T>>(action));

			foreach (var action in initialPossibleActions)
			{
				unvisitedPathes.Add(0, action);
			}

			while (unvisitedPathes.HasElements)
			{
				var path = unvisitedPathes.Get();

				var reachedByPath = path.Reverse().Aggregate(initialState, (current, action) => action.Execute(current));
				if (visitedStates.Contains(reachedByPath))
					continue;
				if (planningStateComparer.Equals(reachedByPath, goalState))
					return path.Reverse();

				visitedStates.Add(reachedByPath);

				var plans = planningActions.Where(action => action.CanExecute(reachedByPath));

				foreach (var action in plans)
				{
					var distance = planningStateComparer.Distance(action.Execute(reachedByPath), goalState);
					var plan = path.AddChild(action, distance);
					unvisitedPathes.Add(plan.Cost, plan);
				}
			}
			return Enumerable.Empty<IPlanningAction<T>>();
		}

		private IPrioritized<double, S> UnvisitedPathes<S>()
		{
			switch (planningMethod)
			{
				case PlanningMethod.BreadthFirst:
					return new PrioritizedQueue<double, S>();
				default:
					return new PrioritizedStack<double, S>();
			}
		}
	}
}