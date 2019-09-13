using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ESFGaivotas.Views.Pages
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            ViewModelLocator.SetAutowireViewModel(this, true);
        }
    }
}
