using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ESFGaivotas.Services
{
    // This service is implemented on Android!
    public interface ILoadingService
    {
        void Init(ContentView loadingIndicatorView = null);
        void Show();
        void Hide();
    }
}
