namespace NutriApp.Goal;

using System.Collections.Generic;
using NutriApp.Workout;

public class MaintainWeightGoal : IGoal
{
    private readonly GoalController controller;
    public double WeightGoal { get; }
    public int DailyCalorieGoal { get; }

    public MaintainWeightGoal(GoalController controller, double weightGoal)
    {
        this.controller = controller;
        this.WeightGoal = weightGoal;
        this.DailyCalorieGoal = 2500;
    }

    public bool CheckWeight(double userWeight) {
        int thresh = 5;
        if (userWeight <= WeightGoal - thresh)
        {
            controller.Goal = new LoseWeightGoal(controller, WeightGoal);
            return true;
        }
        else if (userWeight >= WeightGoal + thresh)
        {
            controller.Goal = new GainWeightGoal(controller, WeightGoal);
            return true;
        }
        return false;
     }

    public void IncorporateFitness(List<Workout> recommendedWorkouts) { }
}