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
        (Guid, User) data = UserControllerCreateUser(danny, "hii", ctl);   
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
        (Guid, User) data = UserControllerCreateUser(danny, "hii", ctl);   

        var adapter = new JSONAdapter();
        saveSystem.SetFileType(new JSONAdapter());
        saveSystem.SaveUser(saveSystem.CreateNewestFolderName(danny.UserName));
        
        ctl.Logout(data.Item1, adapter.GetFileType());

        var loginData = ctl.Login(danny.UserName, "hii");
        Assert.AreEqual(data.Item2, loginData.Item2);
    }

    [TestMethod]
    public void TestSaveLoadPasswords()
    {
        User danny = new User("dannytga", "danny", 72, DateTime.Now, "I am realllllllly tallll");
        User dan = new User("noobles", "dan", 2, DateTime.Now, "I am a gamer");


        if (true)//scoping
        {
            string path = SaveSystem.SavePath + "\\userLogins.json";
            CreateDirectory();//clear data of previous tests

            var saveSystem = new SaveSystem();
            UserController ctl = new UserController(saveSystem);
            saveSystem.SubscribeSaveable(ctl);
            saveSystem.SetFileType(new JSONAdapter());

            UserControllerCreateUser(danny, "hii", ctl);
            UserControllerCreateUser(dan, "pew pew", ctl);

            saveSystem.SaveController();
        }

        if (true)
        {
            var saveSystem = new SaveSystem();
            UserController ctl = new UserController(saveSystem);
            saveSystem.SubscribeSaveable(ctl);
            saveSystem.LoadController();
            saveSystem.SetFileType(new JSONAdapter());

            Assert.AreEqual(danny, ctl.Login(danny.UserName, "hii").Item2);
            Assert.AreEqual(dan, ctl.Login(dan.UserName, "pew pew").Item2);

            bool invalidLogin = false; //set to true in try catch to make sure that the user is invalid
            try
            {
                ctl.Login("daniel", "daniel is weird");
            }
            catch (InvalidUsernameException e)
            {
                invalidLogin = true;
            }
            
            Assert.IsTrue(invalidLogin);
        }
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

        //test if the user exists
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
        (Guid, User) data = UserControllerCreateUser(danny, "hii", ctl);
        ctl.Logout(data.Item1, "json");
        
        //test valid password
        try
        {
            ctl.Login("dannytga", "hi");
        }
        catch (InvalidPasswordException e)
        {
            loggedIn = true;
        }
        
        Assert.IsTrue(loggedIn);//test login invalid password
        
        //make sure that the login goes through
        try
        {
            ctl.Login("dannytga", "hii");
        }
        catch (InvalidPasswordException e)
        { 
            Assert.Fail();
        }
        
        //test if it loads the user correctly
        Assert.AreEqual(danny, ctl.Login("dannytga", "hii").Item2);
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

    private (Guid, User) UserControllerCreateUser(User user, string password, UserController ctl)
    {
        return ctl.CreateUser(user.UserName, password, user.Height, user.Birthday, user.Name, user.Bio);
        
    }
}