using System;

namespace NutriApp.Notifications;

public class Notification
{
    private DateTime timestamp;
    private string contents;
    private string url;
    private string buttonText;

    /// <summary>
    /// The date/time at which the notification was created (not necessarily when
    /// it was sent).
    /// </summary>
    public DateTime Timestamp => timestamp;

    /// <summary>
    /// The text displayed in the notification.
    /// </summary>
    public string Contents => contents;

    /// <summary>
    /// The URL the client will send when the notification is interacted with. This can either
    /// lead to another webpage, or it can be an API call.
    /// </summary>
    public string Url => url;

    /// <summary>
    /// Text to display on notification's button (e.g. "Accept", "View", etc.)
    /// </summary>
    public string ButtonText => buttonText;

    /// <summary>
    /// Has the user read this notification?
    /// </summary>
    public bool Read { get; set; }

    public Notification(string contents, string url, string buttonText)
    {
        this.timestamp = DateTime.Now;
        this.contents = contents;
        this.url = url;
        this.buttonText = buttonText;
        this.Read = false;
    }
}