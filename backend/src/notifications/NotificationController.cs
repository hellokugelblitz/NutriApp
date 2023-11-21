using System.Collections.Generic;

namespace NutriApp.Notifications;

public class NotificationController
{
    private static NotificationController instance;
    private Dictionary<User, Notification[]> pendingNotifications;

    private NotificationController()
    {
        instance = this;
        pendingNotifications = new Dictionary<User, Notification[]>();
    }

    /// <summary>
    /// The singleton instance of the NotificationController. Will create an instance
    /// if one does not exist already.
    /// </summary>
    public static NotificationController Instance
    {
        get
        {
            if (instance == null) new NotificationController();
            return instance;
        }
    }

    /// <summary>
    /// Generates a new notification to be sent out to the given recipients.
    /// </summary>
    public void CreateNotification(string contents, string url, User[] recipients)
    {

    }

    private void NotifyOnlineUsers()
    {

    }

    /// <summary>
    /// Sends all pending notifications to the given user.
    /// </summary>
    public void SendPendingNotifications(User user)
    {
        
    }
}