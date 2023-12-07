using System;
using Moq;
using NutriApp.Save;
using NutriApp.Teams;

namespace NutriAppTest.Teams;

[TestClass]
public class TestTeamController
{
    [TestMethod]
    public void TestCreateGetTeam()
    {
        Mock<App> mockApp = new Mock<App>(2);
        TeamController teamCtrl = new TeamController(mockApp.Object, null);
        string teamName = "cool team";

        Assert.IsTrue(teamCtrl.CreateTeam(teamName) is not null);   // create team
        Assert.IsNotNull(teamCtrl.GetTeam(teamName));   // get that team
        Assert.IsFalse(teamCtrl.CreateTeam(teamName) is not null);  // disallow duplicate team

        Team team = teamCtrl.GetTeam(teamName);
        User user = new User("thatnoobles", "dan", 70, new System.DateTime(2004, 04, 25), "bio");

        team.AddMember(user.UserName);
        Assert.AreEqual(1, team.Members.Length);

        team.RemoveMember(user.UserName);
        Assert.AreEqual(0, team.Members.Length);
    }

    [TestMethod]
    public void TestInvite()
    {
        App app = new App(2);
        TeamController teamCtrl = new TeamController(app, null);

        (Guid, User) userData = app.UserControl.CreateUser("thatnoobles", "password", 70, new System.DateTime(2004, 04, 25), "dan", "bio");
        User user = userData.Item2;

        string teamName = "cool team";
        teamCtrl.CreateTeam(teamName);

        // Sending invite to user
        string inviteCode = teamCtrl.CreateInvite(user.UserName, teamName);
        Assert.AreEqual(8, inviteCode.Length);
        Assert.IsTrue(teamCtrl.ValidateInviteCode(inviteCode));

        Team team = teamCtrl.GetTeam(teamName);

        // User has received invite notification
        Assert.AreEqual(1, user.Notifications.Length);

        // Uesr attempts to join team with invalid invite code
        teamCtrl.AddMember(user.UserName, "aaaaaaaa");
        Assert.AreEqual(0, team.Members.Length);
        Assert.IsTrue(teamCtrl.ValidateInviteCode(inviteCode));

        // User accepts the invite with correct invite code
        teamCtrl.AddMember(user.UserName, inviteCode);
        Assert.AreEqual(1, team.Members.Length);
        Assert.IsFalse(teamCtrl.ValidateInviteCode(inviteCode));  // invite codes should only work once
    }

    [TestMethod]
    public void TestSaveLoad()
    {
        Team t1;
        Team t2;
        if (true)//scoping
        {
            SaveSystem saveSystem = new SaveSystem();
            saveSystem.SetFileType(new JSONAdapter());
            TeamController ctl = new TeamController(null, saveSystem);
            saveSystem.SubscribeSaveable(ctl);

            ctl.CreateTeam("team1");
            ctl.CreateTeam("team2");

            t1 = ctl.GetTeam("team1");
            t2 = ctl.GetTeam("team2");
            
            t1.AddMember("dannytga");
            t1.AddMember("thatnoobles");
            t1.AddMember("racer_aw_06");
            t2.AddMember("crazyformccafe");
            t2.AddMember("hellokugelblitz");

            saveSystem.SaveController();
        }
        
        if (true)//scoping
        {
            SaveSystem saveSystem = new SaveSystem();
            saveSystem.SetFileType(new JSONAdapter());
            TeamController ctl = new TeamController(null, saveSystem);
            saveSystem.SubscribeSaveable(ctl);

            saveSystem.LoadController();
            Assert.AreEqual(t1, ctl.GetTeam("team1"));
            Assert.AreEqual(t1, ctl.GetTeam("team1"));
        }
    }
}