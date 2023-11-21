using System;

namespace NutriApp.UI;

public static class ParserUtils
{
    public static T GetInputFromConsole<T>(string msg, string failMsg = null)
    {
        var input = default(T);

        while (input?.Equals(default(T)) ?? true)
        {
            try
            {
                Console.WriteLine(msg);
                input = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
            }
            catch (Exception)
            {
                Console.WriteLine(failMsg ?? "That was not a valid input");
            }
        }

        return input;
    }
}