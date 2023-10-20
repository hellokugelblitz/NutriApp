using System;
using System.ComponentModel.Design;

namespace NutriApp.UI;

/// <summary>
/// UIController class manages the UI and the menu navigation.
/// </summary>
class UIController
{
    private Menu _menu;
    private App _app;

    public Menu menu
    {
        get => _menu;
        set => _menu = value;
    }
    public App app
    {
        get => _app;
        set => _app = value;
    }

    /// <summary>
    /// Constructor for UIController.
    /// </summary>
    /// <param name="app"></param>
    public UIController(App app)
    {
        _menu = new MainMenu(this); // Defaults as main menu
        this.app = app;
    }
}