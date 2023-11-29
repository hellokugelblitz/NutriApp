namespace NutriApp.Save;

public interface ISaveableController
{
    /// <summary>
    /// Saves the user with the username given in the foldername. Each controller will specify the filename under the folder that they are saving to.
    /// It should also remove the user from the associated dictionaries.
    /// </summary>
    /// <param name="folderName">name of folder. Has to be in format {username}-{fileType}-{identifer(int)}</param>
    void SaveUser(string folderName);
    
    /// <summary>
    /// loads the user with the username given in the foldername. It will find the file with the largest identifer. Each controller will specify the filename under the folder that they are loading from.
    /// </summary>
    /// <param name="folderName">name of folder. Has to be in format {username}-{fileType}-{identifer(int)}</param>
    void LoadUser(string folderName);
    
    
    /// <summary>
    /// When the application is closed. SaveController is called. Every controller will save the data that is for the entire controller instead of a specific user
    /// </summary>
    void SaveController();
    
    /// <summary>
    /// When the application is opened. LoadController is called after all the controllers have been subscribed. Every controller will load the data that is for the entire controller instead of a specific user
    /// </summary>
    void LoadController();

    /// <summary>
    /// adds the user to the associated dictionaries if they are new
    /// </summary>
    /// <param name="user"></param>
    void AddNewUser(User user);
}