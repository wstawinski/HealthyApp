using HealthyApp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Android.Widget.ListView), typeof(CustomListViewRenderer))]
namespace HealthyApp.Renderers
{
    class CustomListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var listView = Control as Android.Widget.ListView;
                listView.NestedScrollingEnabled = true;
            }
        }
    }
}