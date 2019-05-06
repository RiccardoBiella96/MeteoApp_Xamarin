using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.FirebasePushNotification;

namespace MeteoApp.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }

            FirebasePushNotificationManager.Initialize(this, true);
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) => { };
        }
    }
}