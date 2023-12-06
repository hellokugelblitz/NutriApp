using System.Collections.Generic;

namespace NutriApp.Notifications;

public class NotificationController
{
    private static NotificationController instance;
    private Dictionary<string, List<Notification>> pendingNotifications;  // key is username (guaranteed unique)

    private NotificationController()
    {
        instance = this;
        pendingNotifications = new Dictionary<string, List<Notification>>();
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

    public App AppInstance { private get; set; }

    /// <summary>
    /// Generates a new notification to be sent out to the given recipients.
    /// </summary>
    public void CreateNotification(string contents, string url, User[] recipients)
    {
        foreach (User user in recipients)
        {
            if (!pendingNotifications.ContainsKey(user.UserName))
                pendingNotifications.Add(user.UserName, new List<Notification>());

            Notification notification = new Notification(contents, url);
            pendingNotifications[user.UserName].Add(notification);
        }

        NotifyOnlineUsers();
    }

    private void NotifyOnlineUsers()
    {
        foreach (string username in pendingNotifications.Keys)
        {
            User user = AppInstance.UserControl.GetUser(username);
            
            // If user is offline, move onto the next user
            if (user is null) continue;

            SendPendingNotifications(user);
        }
    }

    /// <summary>
    /// Sends all pending notifications to the given user.
    /// </summary>
    public void SendPendingNotifications(User user)
    {
        // if (!pendingNotifications.ContainsKey(user.UserName)) return;

        // Notification[] notifications = pendingNotifications[user.UserName].ToArray();
        
        // foreach (Notification notification in notifications)
        //     user.ReceiveNotification(notification);

        // pendingNotifications[user.UserName].Clear();

        // TODO: also send server-side event to notify client in real time
    }
}