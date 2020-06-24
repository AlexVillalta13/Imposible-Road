using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationsManager : MonoBehaviour
{
    [SerializeField] string title = "Free gift now";
    [SerializeField] string text = "Open the game to get a free gift";
    int hoursToNotification = 12;


    Notifications notifications;

    private void Awake()
    {
        notifications = GetComponent<Notifications>();
    }

    public void SendNotification()
    {
        if(notifications != null)
        {
            notifications.SendNotification(title, text, System.DateTime.Now.AddHours(hoursToNotification));
        }
    }
}
