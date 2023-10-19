using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class PTAddWorkoutInvoker : CommandInvoker<Workout.Workout>
{
    private WorkoutIntensity workoutIntensity;

    public PTAddWorkoutInvoker(Command<Workout.Workout> command) : base(command) { }

    public override void Invoke()
    {
        Console.WriteLine("Enter duration of workout: ");
        int duration = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the intensity of the workout (low, medium, high): ");
        string intensity = Console.ReadLine();

        switch (intensity)
        {
            case "low":
                workoutIntensity = WorkoutIntensity.LOW;
                break;
            case "medium":
                workoutIntensity = WorkoutIntensity.MEDIUM;
                break;
            case "high":
                workoutIntensity = WorkoutIntensity.HIGH;
                break;
            default:
                throw new ArgumentException("Invalid intensity");
        }

        command.Execute(new Workout.Workout(duration, workoutIntensity));
    }
}