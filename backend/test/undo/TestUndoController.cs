using System;
using NutriApp.Save;

namespace NutriApp.Undo;

[TestClass]
public class TestUndoController
{
    private App app;
    private User user = new User("test", "test", 72, DateTime.Now, "test");
    private UserController userController;
    private SaveSystem saveSystem;
    private Guid sessionKey;
    private UndoCommand undoCommand;

    public void Setup()
    {
        app = new App(2);
        saveSystem = new SaveSystem();
        userController = new UserController(saveSystem);

        (sessionKey, user) = userController.CreateUser(user, "test");
        undoCommand = new UndoSetWeight(app, user);

        app.HistoryControl.AddNewUser(user);
    }

    [TestMethod]
    public void TestAdd()
    {
        Setup();

        userController.AddUndoCommand(sessionKey, undoCommand);

        Assert.IsNotNull(userController.GetUndoStack(sessionKey));
        Assert.AreEqual(1, userController.GetUndoStack(sessionKey).UndoStack.Count);
    }

    [TestMethod]
    public void TestUndo()
    {
        Setup();

        app.HistoryControl.SetWeight(100, user.Name);
        Assert.AreEqual(100, app.HistoryControl.CurrentWeight(user.Name));

        userController.AddUndoCommand(sessionKey, undoCommand);
        Assert.AreEqual(1, userController.GetUndoStack(sessionKey).UndoStack.Count);

        userController.Undo(sessionKey);

        Assert.AreEqual(0, userController.GetUndoStack(sessionKey).UndoStack.Count);
    }
}