using Moq;
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
}