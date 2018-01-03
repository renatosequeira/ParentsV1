using Parents.iOS.Renderers;
using Parents.Renderers;

[assembly: Xamarin.Forms.Dependency(typeof(ToastiOS))]
namespace Parents.iOS.Renderers
{
    public class ToastiOS : Toast
    {
        public void Show(string Message)
        {
            Toast_iOS.Toast.MakeText(message).Show();
        }
    }
}