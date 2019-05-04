using NidcApp.Controls;
using NidcApp.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ZoomWebView), typeof(ZoomWebViewRenderer))]

namespace NidcApp.iOS.Renderers
{
    public class ZoomWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            ScalesPageToFit = true;
        }
    }
}