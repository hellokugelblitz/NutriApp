using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class PTAddWorkoutInvoker : CommandInvoker<Workout.Workout>
{
    private WorkoutIntensity workoutIntensity;

    public PTAddWorkoutInvoker(Command<Workout.Workout> command) : base(command) { }

    public override void Invoke()
    {
        Console.WriteLine("Enter the name of the workout");
        string name = Console.ReadLine();

        int duration = -1;
        while (duration < 0)
        {
            try
            {
                Console.WriteLine("Enter duration of workout: ");
                duration = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("please enter a valid number");
            }
        }

        string intensity = "";

        while (intensity == "")
        {
            
            Console.WriteLine("Enter the intensity of the workout (low, medium, high): ");
            intensity = Console.ReadLine().ToLower();
            
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
                    Console.WriteLine("you need to enter a valid intensity");
                    intensity = "";
                    break;
            }
        }
        

        command.Execute(new Workout.Workout(name, duration, workoutIntensity));
    }
}