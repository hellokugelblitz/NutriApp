using System;

namespace NutriApp.UI;

/// <summary>
/// An abstract base class for defining Updaters in the NutriApp UI.
/// </summary>
public abstract class Updater
{
    protected App _app;

    /// <summary>
    /// Constructor for the Updater class.
    /// </summary>
    /// <param name="app"></param>
    public Updater(App app)
    {
        _app = app;
    }

    /// <summary>
    /// Abstract method for updating the UI.
    /// </summary>
    public abstract void Update();
}