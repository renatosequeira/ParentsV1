using Parents.Droid.Renderers;
using Xamarin.Forms;
using Android.Widget;
using Android.Content;

[assembly: Dependency(typeof(Toast_Android))]
namespace Parents.Droid.Renderers
{
    public class Toast_Android: Toast
    {
        public Toast_Android(Context context) : base(context)
        {
        }

        public void Show(string Message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, Message, ToastLength.Long).Show();
        }
    }
}