using Parents.Droid.Renderers;
using Xamarin.Forms;
using System;
using Android.OS;
using Android.Support.Design.Widget;
using Xamarin.Forms.Platform.Android.AppCompat;
using Xamarin.Forms.Platform.Android;
using Android.Content;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(MyTabbedPageRenderer))]
namespace Parents.Droid.Renderers
{
    public class MyTabbedPageRenderer : TabbedPageRenderer
    {
        private TabLayout tabLayout = null;

        public MyTabbedPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            this.tabLayout = (TabLayout)this.GetChildAt(1);

            var selectPosition = this.tabLayout.SelectedTabPosition;

            tabLayout.TabMode = TabLayout.ModeScrollable;
            tabLayout.TabGravity = TabLayout.GravityFill;

            Handler h = new Handler();
            Action myAction = () =>
            {
                tabLayout.GetTabAt(selectPosition).Select();
            };

            h.PostDelayed(myAction, 200);
        }
    }
}