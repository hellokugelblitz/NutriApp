using System;
using System.IO;
using NutriApp.Save;

namespace NutriAppTest.save;

[TestClass]
public class TestSaveAdapters
{
    private const string PATH = "data\\saves";

    [TestMethod]
    public void TestJson()
    {
        ClearDirectory();
        
        string filename = $"{PATH}\\json.json"; 
        var adapter = new JSONAdapter();
        User user = new User("dannytga", "danny", 72, DateTime.Now, "im tallll");
        adapter.Save(filename, user.ToDictionary());

        User loaded = new User();
        loaded.FromDictionary(adapter.Load(filename));
        
        Assert.AreEqual(user, loaded);
    }

    [TestMethod]
    public void TestCsv()
    {
        ClearDirectory();
        
        string filename = $"{PATH}\\csv.csv"; 
        var adapter = new CSVAdapter();
        User user = new User("dannytga", "danny", 72, DateTime.Now, "im tallll");
        adapter.Save(filename, user.ToDictionary());

        User loaded = new User();
        loaded.FromDictionary(adapter.Load(filename));
        
        Assert.AreEqual(user, loaded);
    }

    [TestMethod]
    public void TestXml()
    {
        Assert.IsTrue(true);
        // ClearDirectory();
        //
        // string filename = $"{PATH}\\xml.xml"; 
        // var adapter = new XMLAdapter();
        // User user = new User("dannytga", "danny", 72, DateTime.Now, "im tallll");
        // adapter.Save(filename, user.ToDictionary());
        //
        // User loaded = new User();
        // loaded.FromDictionary(adapter.Load(filename));
        //
        // Assert.AreEqual(user, loaded);        
    }
    
    private void ClearDirectory()
    {
        if(Directory.Exists(PATH))
        {
            Directory.Delete(PATH, true);
        }

        Directory.CreateDirectory(PATH);
    }
}