using System.IO;

namespace NutriAppTest.Save;

public class TestUtils
{
    public static string GetDataPath(string filename)
    {
        return Path.Combine( Directory.GetCurrentDirectory(), "test data/save", filename);
    }
}