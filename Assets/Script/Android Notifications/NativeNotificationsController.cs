using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class NativeNotificationsController : MonoBehaviour
{
    [SerializeField]
    private AndroidNotificationsController androidNotificationController;

    private void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        androidNotificationController.RequestAuthorization();
        androidNotificationController.RegisterNotificationChannel();
    }

    public void SetNotificationSchedule()
    {
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        androidNotificationController.SendNotification(title: "Hora de jogar!", text: "Um novo Sudoku lhe espera!");
        print("Done! Notification sended.");
    }

    
}
