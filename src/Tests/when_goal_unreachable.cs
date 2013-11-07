﻿using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Planning;
using Machine.Specifications;

namespace Tests
{
    [Subject(typeof(Planner))]
    class when_goal_unreachable : BaseContext
    {
        Establish context = () =>
        {
            planner = CreatePlanner();

            initialState = new[]
            {
                new Parameter {Name = "1", Count = 3, IsRequiredExectCount = true, IsRequiredForGoal = true},
                new Parameter {Name = "2", Count = 6, IsRequiredExectCount = true, IsRequiredForGoal = true}
            };
            goalState = new[]
            {
                new Parameter { Name = "1", Count = 5, IsRequiredExectCount = true, IsRequiredForGoal = true },
                new Parameter { Name = "2", Count = 4, IsRequiredExectCount = true, IsRequiredForGoal = true }
            };
        };

        Because of = () =>
            plan = planner.MakePlan(initialState, goalState);

        It should_return_plan = () =>
            plan.ShouldNotBeEmpty();

        It should_contain_3_steps = () =>
            plan.Count().ShouldEqual(3);

        private static Planner planner;
        private static IEnumerable<Parameter> initialState;
        private static IEnumerable<Parameter> goalState;
        private static IEnumerable<IEnumerable<Parameter>> plan;
    }
}