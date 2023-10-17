using System;
using System.ComponentModel.Design;

namespace NutriApp;

class UIController
{
    private Menu _menu;

    public Menu menu
    {
        get => _menu;
        set => _menu = value;
    }

    public UIController(Menu menu)
    {
        this.menu = menu;
    }
}