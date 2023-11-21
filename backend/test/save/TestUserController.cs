using System;
using System.IO;
using Moq;
using Newtonsoft.Json;
using NutriApp.Save;

namespace NutriAppTest.save;

[TestClass]
public class TestUserController
{
    private const string PATH = "data\\save";

    [TestMethod]
    public void TestHashPassword()
    {
        UserController ctl = CreateController();
        string hashed = ctl.HashPassword("hii");
        string expected = "oaOwmHX56ayt5WI+HMpoAAmmyeBFJImTHPpbAEH00pA=";
        Assert.AreEqual(expected, hashed);
    }
    
    [TestMethod]
    public void TestCreateUser()
    {
        UserController ctl = CreateController();
        User danny = new User("dannytga", "danny", 72, DateTime.Now, "I am realllllllly tallll");
        (Guid, User) data = ctl.CreateUser(danny.UserName, "hii", danny.Height, danny.Birthday, danny.Name, danny.Bio);
        Assert.AreEqual(danny, ctl.GetUser(data.Item1));
    }
    
    [TestMethod]
    public void TestSaveLoad()
    {
        CreateDirectory();
        
        User danny = new User("dannytga", "danny", 72, DateTime.Now, "I am realllllllly tallll");
        SaveSystem saveSystem = new SaveSystem();
        UserController ctl = new UserController(saveSystem);
        saveSystem.SubscribeSaveable(ctl);
        (Guid, User) data = ctl.CreateUser(danny.UserName, "hii", danny.Height, danny.Birthday, danny.Name, danny.Bio);

        var adapter = new JSONAdapter();
        saveSystem.SetFileType(new JSONAdapter());
        saveSystem.Save(saveSystem.CreateNewestFolderName(danny.UserName));
        
        ctl.Logout(data.Item1, adapter.GetFileType());

        var loginData = ctl.Login(danny.UserName, "hii");
        Assert.AreEqual(data.Item2, loginData.Item2);
    }

    [TestMethod]
    public void Test_()
    {
        User danny = new User("dannytga", "danny", 72, DateTime.Now, "I am realllllllly tallll", "t1");

        var str = JsonConvert.SerializeObject(danny);
    }

    [TestMethod]
    public void TestGetNewestFolder()
    {
        for (int i = 0; i < 5; i++)
        {
            Directory.CreateDirectory($"{SaveSystem.SavePath}\\dannytga-json-{i}");
        }
        
        Directory.CreateDirectory($"{SaveSystem.SavePath}\\danny-json-{8}");

        SaveSystem saveSystem = new SaveSystem();
        var newest = saveSystem.GetNewestFolder("dannytga");
        Assert.AreEqual($"{SaveSystem.SavePath}\\dannytga-json-4", newest);
        
        CreateDirectory();
    }
    
    [TestMethod]
    public void TestLogin()
    {
        CreateDirectory();//wipe any previous tests
        
        bool loggedIn = false;
        UserController ctl = CreateController();

        try
        {
            ctl.Login("dannytga", "hii");
        }
        catch (InvalidUsernameException e)
        {
            loggedIn = true;
        }
        
        Assert.IsTrue(loggedIn);//test login invalid username

        loggedIn = false;
        User danny = new User("dannytga", "danny", 72, DateTime.Now, "I am realllllllly tallll");
        (Guid, User) data = ctl.CreateUser(danny.UserName, "hii", danny.Height, danny.Birthday, danny.Name, danny.Bio);
        ctl.Logout(data.Item1, "json");
        
        try
        {
            ctl.Login("dannytga", "hi");
        }
        catch (InvalidPasswordException e)
        {
            loggedIn = true;
        }
        
        Assert.IsTrue(loggedIn);//test login invalid password
        
        try
        {
            ctl.Login("dannytga", "hii");
        }
        catch (InvalidPasswordException e)
        { 
            Assert.Fail();
        }
        
        Assert.AreEqual(danny, ctl.Login("dannytga", "hii").Item2);
        //need to test user exists and doesnt
    }
    
    

    private UserController CreateController()
    {
        var saveSystem = new SaveSystem();
        UserController ctl = new UserController(saveSystem);
        saveSystem.SubscribeSaveable(ctl);
        return ctl;
    }
    
    private void CreateDirectory()
    {
        if(Directory.Exists(PATH))
        {
            Directory.Delete(PATH, true);
        }

        Directory.CreateDirectory(PATH);
    }
}