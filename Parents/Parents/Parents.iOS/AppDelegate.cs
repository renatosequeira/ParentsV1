using Foundation;
using Plugin.Toasts;
using UIKit;
using Xamarin.Forms;

namespace Parents.iOS
{

    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            DependencyService.Register<ToastNotification>(); // Register your dependency
            ToastNotification.Init();

            Xamarin.FormsMaps.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }      
    }


}
