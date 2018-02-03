
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Plugin.Toasts;
using Android.Views;
using Android.Content;
using PushNotification.Plugin;
using Parents.Droid.Helpers;
using Parents.Helpers;
using FormsPinView.Droid;
using ButtonCircle.FormsPlugin.Droid;

namespace Parents.Droid
{
    [Activity(Label = "Parents", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Context AppContext;
        public static Android.Support.V7.Widget.Toolbar ToolBar { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            DependencyService.Register<ToastNotification>(); // Register your dependency
            ToastNotification.Init(this);

            // If you are using Android you must pass through the activity
            ToastNotification.Init(this);

            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    RequestedOrientation = ScreenOrientation.Portrait;
                    break;
                case TargetIdiom.Tablet:
                    RequestedOrientation = ScreenOrientation.Landscape;
                    break;
            }
            Xamarin.FormsMaps.Init(this, bundle);

            PinItemViewRenderer.Init();

            ButtonCircleRenderer.Init();

            AppContext = this.ApplicationContext;

            //TODO: Initialize CrossPushNotification Plugin
            //TODO: Replace string parameter with your Android SENDER ID
            //TODO: Specify the listener class implementing IPushNotificationListener interface in the Initialize generic
            CrossPushNotification.Initialize<CrossPushNotificationListener>("729968483014");

            StartPushService();

            LoadApplication(new App());

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            ToolBar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            return base.OnCreateOptionsMenu(menu);
        }

        public static void StartPushService()
        {
            AppContext.StartService(new Intent(AppContext, typeof(PushNotificationService)));

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {

                PendingIntent pintent = PendingIntent.GetService(AppContext, 0, new Intent(AppContext, typeof(PushNotificationService)), 0);
                AlarmManager alarm = (AlarmManager)AppContext.GetSystemService(Context.AlarmService);
                alarm.Cancel(pintent);
            }
        }

        public static void StopPushService()
        {
            AppContext.StopService(new Intent(AppContext, typeof(PushNotificationService)));
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat)
            {
                PendingIntent pintent = PendingIntent.GetService(AppContext, 0, new Intent(AppContext, typeof(PushNotificationService)), 0);
                AlarmManager alarm = (AlarmManager)AppContext.GetSystemService(Context.AlarmService);
                alarm.Cancel(pintent);
            }
        }

    }

}


