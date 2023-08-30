using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using Unity.Notifications.Android;
using System;

public class AndroidNotificationsController : MonoBehaviour
{
    private void Start()
    {
        print(DateTime.Now.ToString());
    }


    public void RequestAuthorization()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATION"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATION");
        }
    }

    public void RegisterNotificationChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications"
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(string title, string text)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = System.DateTime.Today.AddDays(1);
        notification.RepeatInterval = new System.TimeSpan(days: 1, hours: 8, minutes: 0, seconds: 0);
        notification.ShowTimestamp = true;

        AndroidNotificationCenter.SendNotification(notification, channelId: "default_channel");
    }
    
    public void CheckPreviousNotification()
    {

    }
}
