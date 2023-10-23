using System;
using System.Collections.Generic;

namespace NutriApp.UI;


/// <summary>
/// UIController class manages the UI and the menu navigation.
/// </summary>
public class UIController

{
    private Menu _menu;
    private App _app;

    public Menu menu
    {
        get => _menu;
        set
        {
            _menu = value;
            _menu.Handle();
        }
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
        this.app = app;
        _menu = new MainMenu(this); // Defaults as main menu
        menu = new MainMenu(this);
    }
}