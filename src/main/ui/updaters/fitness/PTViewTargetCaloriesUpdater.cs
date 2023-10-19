using System;

namespace NutriApp.UI;

class PTViewTargetCaloriesUpdater : Updater
{
    public PTViewTargetCaloriesUpdater(AddWorkoutCommand addWorkoutCommand, App app): base(app)
    {
        addWorkoutCommand.Subscribe(Update);
    }

    public override void Update()
    {
        
    }
}