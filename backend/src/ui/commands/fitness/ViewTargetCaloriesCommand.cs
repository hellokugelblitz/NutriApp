using System;
using NutriApp.Workout;

namespace NutriApp.UI;

class ViewTargetCaloriesCommand : Command<None>
{
    public override void Execute(None userinput)
    {
        onFinished?.Invoke(); // Front end will view the target calories
    }
}