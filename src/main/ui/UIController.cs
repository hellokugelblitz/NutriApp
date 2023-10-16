using System;
using System.ComponentModel.Design;

namespace NutriApp
{
    class UIController
    {
        private Menu menu;

        public UIController(Menu menu)
        {
            this.menu = menu;
        }

        public void SetMenu(Menu menu)
        {
            this.menu = menu;
        }
}