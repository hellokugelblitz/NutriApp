using System;
using System.Threading;

namespace NutriAppTest.main;

[TestClass]
public class TestApp
{
    [TestMethod]
    public void TestDayEnd()
    {
        //Assert.AreEqual(true, true);
        App app = new App(1d / 120);
        int interations = 0;
        app.SubscribeDayEndEvent((date) => interations++);
        Thread.Sleep(1100);
        Assert.AreEqual(2, interations);
    }
}