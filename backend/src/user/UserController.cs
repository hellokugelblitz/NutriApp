using System;
using NutriApp.Save;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NutriApp;

public class UserController : ISaveable
{
    private Dictionary<string, string>
        _userLoginInfo = new(); //first string is username, second string is hashed password

    //dictionary of guid
    private Dictionary<Guid, User> _users = new();
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
                _saveSystem.Load(_saveSystem.GetNewestFolder(username));
                
                Guid userGuid = Guid.NewGuid();
                _loadedUsers.Remove(username, out User user);
                AddUserToDictionaries(userGuid, user);
                return (userGuid, user);
            }

            throw new InvalidPasswordException();
        }

        throw new InvalidUsernameException();
    }

    public void Logout(Guid sessionKey)
    {
        _users.Remove(sessionKey);
        //save user
    }

    public void Save(string folderName)
    {
        //_saveSystem.GetFileSaver().Save(folderName + "\\user", UserToDictionary());
    }

    private Dictionary<string, string> UserToDictionary(User user)
    {
        Dictionary<string, string> data = new();
        data["UserName"] = user.UserName;
        data["Name"] = user.Name;
        data["Height"] = user.Height.ToString();
        data["Birthday"] = user.Birthday.ToString();
        data["Bio"] = user.Bio;
        data["TeamName"] = user.TeamName;
        
        return data;
    }

    // public (User, Guid) LoadUser(string folderName)
    // {
    //     return (null, Guid.Empty);
    // }

    public void Load(string folderName)
    {
        Dictionary<string, string> data = _saveSystem.GetFileSaver().Load(SaveSystem.GetFullPath(folderName, "user"));
        User user = new User(
            data["username"],
            data["name"],
            Int32.Parse(data["height"]),
            DateTime.Parse(data["birthday"]),
            data["bio"],
            data["teamName"]);

        _loadedUsers[data["username"]] = user;

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

        if (password != "")
        {
            _userLoginInfo[user.UserName] = HashPassword(password);
        }
    }
}