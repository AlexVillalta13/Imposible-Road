using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;


public class Notifications : MonoBehaviour
{
#if UNITY_ANDROID
    AndroidNotificationChannel defaultNotificationChannel;
    int notificationID;

    void Start()
    {
        defaultNotificationChannel = new AndroidNotificationChannel()
        {
            Id = "default_Channel_ID",
            Name = "default_Channel",
            Description = "For All Notifications",
            Importance = Importance.Default,
        };

        AndroidNotificationCenter.RegisterNotificationChannel(defaultNotificationChannel);
    }

    public void SendNotification(string title, string text, DateTime fireTime)
    {
        AndroidNotification notification = new AndroidNotification()
        {
            Title = title,
            Text = text,
            SmallIcon = "icon_small",
            LargeIcon = "icon_large",
            FireTime = fireTime,
        };

        notificationID = AndroidNotificationCenter.SendNotification(notification, defaultNotificationChannel.Id);
    }
#elif UNITY_IOS

#endif
}
