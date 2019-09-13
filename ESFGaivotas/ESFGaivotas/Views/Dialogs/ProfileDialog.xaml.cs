using ESFGaivotas.Model.Element;
using ESFGaivotas.Services;
using Prism.Events;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESFGaivotas.Views.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileDialog : BaseDialog
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IPageDialogService _pageDialogService;
        private readonly IUserService _userService;

        public ProfileDialog()
        {
            InitializeComponent();

            _eventAggregator = App.Resolve<IEventAggregator>();
            _pageDialogService = App.Resolve<IPageDialogService>();
            _userService = App.Resolve<IUserService>();

            FirstName = _userService.User.FirstName;
        }

        #region Bindable Properties

        public string FirstName
        {
            get { return (string)GetValue(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }
        public static readonly BindableProperty FirstNameProperty = BindableProperty.Create(nameof(FirstName), typeof(string), typeof(ProfileDialog), default(string));

        public ObservableCollection<DebrisElement> DebrisCollection
        {
            get { return (ObservableCollection<DebrisElement>)GetValue(DebrisCollectionProperty); }
            set { SetValue(DebrisCollectionProperty, value); }
        }
        public static readonly BindableProperty DebrisCollectionProperty = BindableProperty.Create(nameof(DebrisCollection), typeof(ObservableCollection<DebrisElement>), typeof(ProfileDialog), default(ObservableCollection<DebrisElement>));

        public double Weight
        {
            get { return (double)GetValue(WeightProperty); }
            set { SetValue(WeightProperty, value); }
        }
        public static readonly BindableProperty WeightProperty = BindableProperty.Create(nameof(Weight), typeof(double), typeof(ProfileDialog), default(double));

        #endregion
    }
}