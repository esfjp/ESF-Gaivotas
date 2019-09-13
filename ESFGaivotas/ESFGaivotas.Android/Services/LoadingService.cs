using Android.App;
using Android.Graphics.Drawables;
using Android.Views;
using ESFGaivotas.Services;
using ESFGaivotas.Views.Dialogs;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ESFGaivotas.Droid.Services
{
    public class LoadingService : ILoadingService
    {
        private Android.Views.View nativeView;
        private Dialog dialog;
        private bool isInitialized;

        public void Init(ContentView loadingIndicatorView = null)
        {
            // check if the page parameter is available
            if (loadingIndicatorView != null)
            {
                // build the loading page with native base
                loadingIndicatorView.Parent = Xamarin.Forms.Application.Current.MainPage;

                loadingIndicatorView.Layout(new Rectangle(0, 0,
                    Xamarin.Forms.Application.Current.MainPage.Width,
                    Xamarin.Forms.Application.Current.MainPage.Height));

                var renderer = loadingIndicatorView.GetOrCreateRenderer();

                nativeView = renderer.View;

                dialog = new Dialog(CrossCurrentActivity.Current.Activity);
                dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
                dialog.SetCancelable(false);
                dialog.SetContentView(nativeView);
                Window window = dialog.Window;
                window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                window.ClearFlags(WindowManagerFlags.DimBehind);
                window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

                isInitialized = true;
            }
        }

        public void Show()
        {
            // check if the user has set the page or not
            if (!isInitialized)
                Init(new LoadingDialog()); // set the default page

            // showing the native loading page
            dialog.Show();
        }

        public void Hide()
        {
            dialog.Hide();
        }
    }

    internal static class PlatformExtension
    {
        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable)
        {
            var renderer = Platform.GetRenderer(bindable);
            if (renderer == null)
            {
                renderer = Platform.CreateRendererWithContext(bindable, CrossCurrentActivity.Current.Activity);
                Platform.SetRenderer(bindable, renderer);
            }
            return renderer;
        }
    }
}