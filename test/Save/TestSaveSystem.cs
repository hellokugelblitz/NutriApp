using System;
using System.Collections.Generic;
using System.IO;
using NutriApp.Save;

namespace NutriAppTest.Save;

[TestClass]
public class TestSaveSystem
{
    [TestMethod]
    public void TestLoadJsonAdapter()
    {
        var adapter = new JSONAdapter();
        string str = TestUtils.GetDataPath("test.json");
        var data = adapter.Load(str);
        Dictionary<string, string> expected =  new (){{"name", "danny"}, {"height", "72"}, {"things", "[1, 2, 3]"}};
        var t = expected.Keys.Equals(data.Keys);
        var b = expected.Values.Equals(data.Values);
        
        Assert.IsTrue(t && b);
        //adapter.Load()
    }
}