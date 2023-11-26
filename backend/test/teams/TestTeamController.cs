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
        TeamController teamCtrl = new TeamController(mockApp.Object);
        string teamName = "cool team";

        Assert.IsTrue(teamCtrl.CreateTeam(teamName));   // create team
        Assert.IsNotNull(teamCtrl.GetTeam(teamName));   // get that team
        Assert.IsFalse(teamCtrl.CreateTeam(teamName));  // disallow duplicate team

        Team team = teamCtrl.GetTeam(teamName);
        User user = new User("thatnoobles", "dan", 70, new System.DateTime(2004, 04, 25), "bio");

        team.AddMember(user);
        Assert.AreEqual(1, team.Members.Length);

        team.RemoveMember(user);
        Assert.AreEqual(0, team.Members.Length);
    }

    [TestMethod]
    public void TestInvite()
    {
        App app = new App(2);
        TeamController teamCtrl = new TeamController(app);

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
}