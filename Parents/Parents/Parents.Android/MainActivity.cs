
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Plugin.Toasts;

namespace Parents.Droid
{
    [Activity(Label = "Parents", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
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
    }
}

