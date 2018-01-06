using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Plugin.Toasts;
using Java.Lang;

namespace Parents.Droid
{
    [Activity(Label = "Parents", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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

            LoadApplication(new App());
        }

        //public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        //{
        //    base.OnConfigurationChanged(newConfig);

        //    switch (newConfig.Orientation)
        //    {
        //        case Orientation.Landscape:
        //            switch (Device.Idiom)
        //            {
        //                case TargetIdiom.Phone:
        //                    LockRotation(Orientation.Portrait);
        //                    break;
        //                case TargetIdiom.Tablet:
        //                    LockRotation(Orientation.Landscape);
        //                    break;
        //            }
        //            break;
        //        case Orientation.Portrait:
        //            switch (Device.Idiom)
        //            {
        //                case TargetIdiom.Phone:
        //                    LockRotation(Orientation.Portrait);
        //                    break;
        //                case TargetIdiom.Tablet:
        //                    LockRotation(Orientation.Landscape);
        //                    break;
        //            }
        //            break;
        //    }
        //}

        //private void LockRotation(Orientation orientation)
        //{
        //    switch (orientation)
        //    {
        //        case Orientation.Portrait:
        //            RequestedOrientation = ScreenOrientation.Portrait;
        //            break;
        //        case Orientation.Landscape:
        //            RequestedOrientation = ScreenOrientation.Landscape;
        //            break;
        //    }
        //}
    }
}

