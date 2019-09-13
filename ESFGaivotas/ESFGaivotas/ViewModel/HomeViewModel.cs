using ESFGaivotas.Aggregator;
using ESFGaivotas.Enumerations;
using ESFGaivotas.Model.Element;
using ESFGaivotas.Services;
using ESFGaivotas.Views.Pages;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ESFGaivotas.ViewModel
{
    public class HomeViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService navigationService;
        private readonly IUserService userService;
        private readonly IPageDialogService pageDialogService;

        public HomeViewModel(INavigationService navigationService, IUserService userService, IEventAggregator eventAggregator,
            IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.userService = userService;
            this.pageDialogService = pageDialogService;
            
            eventAggregator.GetEvent<HomeInitializerEvent>().Subscribe(Initialize);
        }

        #region Fields and Properties

        

        #endregion

        #region Bindable Properties

        public ObservableCollection<ReportElement> ReportCollection { get; } = new ObservableCollection<ReportElement>();

        public ObservableCollection<DebrisElement> DebrisCollection { get; } = new ObservableCollection<DebrisElement>();

        public string ProfilePicture
        {
            get { return _profilePicture; }
            set { SetProperty(ref _profilePicture, value); }
        }
        private string _profilePicture;

        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        private string _firstName;

        public double TotalWeight
        {
            get { return _totalWeight; }
            set { SetProperty(ref _totalWeight, value); }
        }
        private double _totalWeight;

        public bool IsReportLoading
        {
            get { return _isReportLoading; }
            set { SetProperty(ref _isReportLoading, value); }
        }
        private bool _isReportLoading;

        public bool IsProfileOpen
        {
            get { return _isProfileOpen; }
            set { SetProperty(ref _isProfileOpen, value); }
        }
        private bool _isProfileOpen = false;

        #endregion

        #region Commanding

        public DelegateCommand OpenProfile => _openProfile ?? (_openProfile = new DelegateCommand(ExecuteOpenProfile));
        private DelegateCommand _openProfile;
        void ExecuteOpenProfile()
        {
            DebrisCollection.Clear();
            var debrisTypes = (EDebrisType[])Enum.GetValues(typeof(EDebrisType));

            foreach (var debrisType in debrisTypes)
            {
                var quantity = 0;
                foreach (var report in ReportCollection)
                    quantity += report.DebrisCollection.FirstOrDefault(l => l.DebrisType == debrisType).Quantity;

                DebrisCollection.Add(new DebrisElement(debrisType, quantity));
            }

            var totalWeight = 0D;
            foreach (var report in ReportCollection)
                totalWeight += report.Report.Weight;

            TotalWeight = totalWeight;
            
            IsProfileOpen = true;
        }

        public DelegateCommand CreateReport => _createReport ?? (_createReport = new DelegateCommand(ExecuteCreateReport));
        private DelegateCommand _createReport;
        async void ExecuteCreateReport()
        {
            var permissionResult = await RequestLocationPermission();
            if (permissionResult)
            {
                await navigationService.NavigateAsync(nameof(ReportPage));
            }
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

        public async void Initialize()
        {
            ProfilePicture = userService.User.ProfilePicture;
            FirstName = userService.User.FirstName;

            await LoadReports();
        }

        private async Task LoadReports()
        {
            IsReportLoading = true;

            var reports = await userService.GetUserReports();

            ReportCollection.Clear();

            foreach (var report in reports)
                ReportCollection.Add(new ReportElement() { Report = report });

            IsReportLoading = false;
        }

        public async Task<bool> RequestLocationPermission()
        {
            try
            {
                var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

                if (permissionStatus != PermissionStatus.Granted)
                {
                    var permissionResult = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);

                    if (permissionResult.ContainsKey(Permission.Location))
                        if (permissionResult[Permission.Location] != PermissionStatus.Granted)
                        {
                            await pageDialogService.DisplayAlertAsync("Warning", "Location permission request has not been granted. The app will have to request it before the tracking service starts!", "Ok");
                            return false;
                        }

                }

                return true;
            }
            catch (Exception ex)
            {
                await pageDialogService.DisplayAlertAsync("Error", "Location permission request has been denied!", "Ok");
                Console.WriteLine($"Location permission request was denied! Error message: \r\n{ex.Message}");
                return false;
            }
        }

        #endregion
    }
}