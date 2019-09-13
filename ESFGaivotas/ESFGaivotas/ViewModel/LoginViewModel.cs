using ESFGaivotas.Aggregator;
using ESFGaivotas.Model.Repository;
using ESFGaivotas.Services;
using ESFGaivotas.Views.Pages;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ESFGaivotas.ViewModel
{
    public class LoginViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IUserService _userService;
        private readonly ILoadingService loadingService;

        public LoginViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IUserService userService, ILoadingService loadingService,
            IEventAggregator eventAggregator)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;
            this._userService = userService;
            this.loadingService = loadingService;
        }

        #region Bindable Properties

        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }
        private string _login = "joaopessoa";

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        private string _password = "gaivotas";

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }
        private bool _isLoading;

        public bool IsCreateUserOpen
        {
            get { return _isCreateUserOpen; }
            set { SetProperty(ref _isCreateUserOpen, value); }
        }
        private bool _isCreateUserOpen = false;

        #endregion

        #region Commanding

        public DelegateCommand LoginAttempt => _loginAttempt ?? (_loginAttempt = new DelegateCommand(ExecuteLoginAttempt, CanAttemptLogin)
            .ObservesProperty(() => IsLoading));
        private DelegateCommand _loginAttempt;
        async void ExecuteLoginAttempt()
        {
            loadingService.Show();

            if (await _userService.LoginAttempt(Login, Password))
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(HomePage)}");
            else
                _pageDialogService.DisplayAlertAsync("Alerta", "Tentativa de login não funcionou!", "Tentar novamente");

            loadingService.Hide();
        }

        #endregion

        #region Navigation Events
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {

        }
        #endregion

        #region Helpers
        
        private bool CanAttemptLogin()
        {
            return !IsLoading;
        }

        #endregion
    }
}
