using System.Collections;
using System.Runtime.Remoting.Messaging;
using Android.App;
using Android.Widget;
using Parents.Droid.Renderers;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace Parents.Droid.Renderers
{
    public class MessageAndroid : IMessage
    {
        public IDictionary Properties => throw new System.NotImplementedException();

        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}