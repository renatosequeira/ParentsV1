
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Plugin.Toasts;
using Android.Views;
using Android.Support.V7.Widget;

namespace Parents.Droid
{
    [Activity(Label = "Parents", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
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
            LoadApplication(new App());
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            ToolBar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            return base.OnCreateOptionsMenu(menu);
        }

    }
}

