using System;

namespace NutriApp.UI
{
    public abstract class Updater
    {
        protected App _app;
        public Updater(App app)
        {
            _app = app;
        }
        
        public abstract void Update();
    }
}