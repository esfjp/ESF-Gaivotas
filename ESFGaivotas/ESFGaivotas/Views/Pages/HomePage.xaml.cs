using ESFGaivotas.Aggregator;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESFGaivotas.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BasePage
    {
        private readonly IEventAggregator _eventAggregator;

        public HomePage(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this._eventAggregator = eventAggregator;
        }

        protected override void OnAppearing()
        {
            this._eventAggregator.GetEvent<HomeInitializerEvent>().Publish();
            base.OnAppearing();
        }
    }
}