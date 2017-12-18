using Android.Content;
using Xamarin.Forms;
using Parents.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Parents.Renderers;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace Parents.Droid.Renderers
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        public CustomButtonRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            //ToDo: Customize Button
            
        }
    }
}