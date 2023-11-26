using System;
using NutriApp.Save;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace NutriApp;

public class UserController : ISaveableController
{
    //TODO serialize passwords 
    private Dictionary<string, string> _userLoginInfo = new(); //first string is username, second string is hashed password

    //dictionary of guid
    private Dictionary<Guid, User> _users = new();
    private Dictionary<string, User> _usersFromUsername = new();
    private ISaveSystem _saveSystem;

    //when I load the user from save system,
    //the load will dump the user here. then login can pull it from here and remove it.
    //I am doing this to make it thread safe
    private Dictionary<string, User> _loadedUsers = new();

    public UserController(ISaveSystem saveSystem)
    {
        _saveSystem = saveSystem;
    }

    public User GetUser(Guid sessionKey)
    {
        if (!_users.ContainsKey(sessionKey)) return null;
        return _users[sessionKey];
    }

    /// <summary>
    /// Get an instance of an online user by their username. Returns null if the user doesn't
    /// exist or is currently offline.
    /// </summary>
    public User GetUser(string username)
    {
        if (!_usersFromUsername.ContainsKey(username)) return null;

        return _usersFromUsername[username];
    }

    public (Guid, User) CreateUser(string username, string password, int height, DateTime birthday,
        string name, string bio)
    {
        User user = new User(username, name, height, birthday, bio);
        Guid userGuid = Guid.NewGuid();
        AddUserToDictionaries(userGuid, user, password);
        return (userGuid, user);
    }

    /// <summary>
    /// logs the user in if they have a file in the system already
    /// </summary>
    /// <param name="username">username of the user</param>
    /// <param name="password">pre-hashed password</param>
    /// <returns>a tuple of a new session key and loaded user</returns>
    /// <exception cref="InvalidPasswordException">throws an exception if the user exists but the password was wrong</exception>
    /// <exception cref="InvalidUsernameException">throws an exception if the user is not in the system</exception>
    public (Guid, User) Login(string username, string password) 
    {
        if (_userLoginInfo.ContainsKey(username))
        {
            if (_userLoginInfo[username] == HashPassword(password))
            {
                _saveSystem.LoadUser(_saveSystem.GetNewestFolder(username));
                
                Guid userGuid = Guid.NewGuid();
                _loadedUsers.Remove(username, out User user);
                AddUserToDictionaries(userGuid, user);
                return (userGuid, user);
            }

            throw new InvalidPasswordException();
        }

        throw new InvalidUsernameException();
    }

    /// <summary>
    /// logs the user out of the system. Saves the user info in a new file.
    /// </summary>
    /// <param name="sessionKey"></param>
    /// <param name="fileType">type of file you want to save to. defaults to current FileFormatType if not given</param>
    public void Logout(Guid sessionKey, string fileType = "")
    {
        _saveSystem.SaveUser(_saveSystem.CreateNewestFolderName(_users[sessionKey].UserName, fileType));
        _users.Remove(sessionKey);
    }

    public void SaveUser(string folderName)
    {
        var split = SaveSystem.SplitFileName(folderName);
        string username = split[0];
        _saveSystem.GetFileSaver().Save(SaveSystem.GetFullPath(folderName,"user"), _usersFromUsername[username].ToDictionary());
    }

    public void LoadUser(string folderName)
    {
        Dictionary<string, string> data = _saveSystem.GetFileSaver().Load(SaveSystem.GetFullPath(folderName, "user"));
        User user = new User();
        user.FromDictionary(data);
        _loadedUsers[user.UserName] = user;
    }

    public void SaveController()
    {
        //will do saving with json
        string filePath = SaveSystem.SavePath + "\\userLogins.json";
        using (var file = File.CreateText(filePath))
        {
            file.WriteLine(JsonConvert.SerializeObject(_userLoginInfo));
        }

        foreach (var pair in _users)
        {
            _saveSystem.SaveUser(_saveSystem.CreateNewestFolderName(pair.Value.UserName));
        }
    }

    public void LoadController()
    {
        string filePath = SaveSystem.SavePath + "\\userLogins.json";
        if(!File.Exists(filePath))return;
        
        using (var file = File.OpenText(filePath))
        {
            _userLoginInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(file.ReadLine());
            
        }
        
    }

    public string HashPassword(string password)
    {
        SHA256 sha = SHA256.Create();
        Byte[] asBytes = Encoding.UTF8.GetBytes(password);
        Byte[] hashed = sha.ComputeHash(asBytes);
        return Convert.ToBase64String(hashed);
    }

    private void AddUserToDictionaries(Guid sessionKey, User user, string password = "")
    {
        _users[sessionKey] = user;
        _usersFromUsername[user.UserName] = user;

        if (password != "")
        {
            _userLoginInfo[user.UserName] = HashPassword(password);
        }
    }
}