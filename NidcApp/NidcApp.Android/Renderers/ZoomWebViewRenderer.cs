using System.ComponentModel;
using Android.Content;
using NidcApp.Controls;
using NidcApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ZoomWebView), typeof(ZoomWebViewRenderer))]

namespace NidcApp.Droid.Renderers
{
    public class ZoomWebViewRenderer : WebViewRenderer
    {
        public ZoomWebViewRenderer(Context context) : base(context) { }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
                return;

            Control.Settings.BuiltInZoomControls = true;
            Control.Settings.DisplayZoomControls = false;
        }
    }
}