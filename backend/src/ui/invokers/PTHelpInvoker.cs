using System;

namespace NutriApp.UI;

public class PTHelpInvoker: Invoker
{
    private Help _help;
    public PTHelpInvoker(Help help)
    {
        _help = help;
    }
    
    public void Invoke()
    {
        foreach (var str in _help.GetOptions())
        {
            Console.WriteLine(str);
        }
    }
}