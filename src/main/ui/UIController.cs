using System;
using System.ComponentModel.Design;

namespace NutriApp.UI;

public class UIController
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

    public UIController(App app)
    {
        _menu = new MainMenu(this);
        this.app = app;
    }
}