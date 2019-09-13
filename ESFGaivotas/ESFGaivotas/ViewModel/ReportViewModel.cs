using ESFGaivotas.Model.Core;
using ESFGaivotas.Model.Element;
using ESFGaivotas.Model.Repository;
using ESFGaivotas.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ESFGaivotas.ViewModel
{
    public class ReportViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService navigationService;
        private readonly ITrackingService trackingService;
        private readonly IUserService userService;
        private readonly ILoadingService loadingService;

        public ReportViewModel(INavigationService navigationService, ITrackingService trackingService, IUserService userService, ILoadingService loadingService)
        {
            this.navigationService = navigationService;
            this.trackingService = trackingService;
            this.userService = userService;
            this.loadingService = loadingService;
        }

        #region Bindable Properties

        public ObservableCollection<DebrisElement> MapDebrisCollection { get; } = new ObservableCollection<DebrisElement>();

        public List<DebrisElement> ReportDebrisCollection
        {
            get { return _reportDebrisCollection; }
            set { SetProperty(ref _reportDebrisCollection, value); }
        }
        private List<DebrisElement> _reportDebrisCollection;

        public double MapRotation
        {
            get { return _mapRotation; }
            set { SetProperty(ref _mapRotation, value); }
        }
        private double _mapRotation;

        public Xamarin.Forms.GoogleMaps.Position MapCenter
        {
            get { return _mapCenter; }
            set { SetProperty(ref _mapCenter, value); }
        }
        private Xamarin.Forms.GoogleMaps.Position _mapCenter;

        public bool IsCollectOpen
        {
            get { return _isCollectOpen; }
            set { SetProperty(ref _isCollectOpen, value); }
        }
        private bool _isCollectOpen = false;

        public bool IsWeightOpen
        {
            get { return _isWeightOpen; }
            set { SetProperty(ref _isWeightOpen, value); }
        }
        private bool _isWeightOpen = false;

        public double Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }
        private double _weight;

        public double Latitude
        {
            get { return _latitude; }
            set { SetProperty(ref _latitude, value); }
        }
        private double _latitude;

        public double Longitude
        {
            get { return _longitude; }
            set { SetProperty(ref _longitude, value); }
        }
        private double _longitude;

        #endregion

        #region Commanding

        public DelegateCommand GoBack => _goBack ?? (_goBack = new DelegateCommand(ExecuteGoBack));
        private DelegateCommand _goBack;
        async void ExecuteGoBack()
            => await this.navigationService.GoBackAsync();

        public DelegateCommand OpenCollect => _openCollect ?? (_openCollect = new DelegateCommand(ExecuteOpenCollect));
        private DelegateCommand _openCollect;
        void ExecuteOpenCollect()
            => IsCollectOpen = true;

        public DelegateCommand<object> AddMapDebris => _addMapDebris ?? (_addMapDebris = new DelegateCommand<object>(ExecuteAddDebris));
        private DelegateCommand<object> _addMapDebris;
        void ExecuteAddDebris(object parameter)
        {
            var debrisListParameter = (List<DebrisElement>)parameter;

            var debrisList = debrisListParameter.Where(l => l.IsSelected == true).ToList();

            foreach (var debris in debrisList)
            {
                debris.IsSelected = false;
                MapDebrisCollection.Add(new DebrisElement(debris.DebrisType, 1) { Latitude = MapCenter.Latitude, Longitude = MapCenter.Longitude });
            }

            IsCollectOpen = false;
        }

        public DelegateCommand SaveReport => _saveReport ?? (_saveReport = new DelegateCommand(ExecuteSaveReport));
        private DelegateCommand _saveReport;
        void ExecuteSaveReport()
        {
            IsWeightOpen = true;

            var reportDebrisCollection = new List<DebrisElement>();
            foreach (var debris in MapDebrisCollection)
            {
                var reportDebris = reportDebrisCollection.FirstOrDefault(l => l.DebrisType == debris.DebrisType);

                if (reportDebris == null) reportDebrisCollection.Add(debris);
                else reportDebris.Quantity += 1;
            }

            ReportDebrisCollection = reportDebrisCollection;
        }

        public DelegateCommand ConfirmReport => _confirmReport ?? (_confirmReport = new DelegateCommand(ExecuteConfirmReport));
        private DelegateCommand _confirmReport;
        async void ExecuteConfirmReport()
        {
            loadingService.Show();

            using (var unitOfWork = new UnitOfWork(new ESFContext()))
            {
                var newReport = new ReportModel()
                {
                    UserId = userService.User.Id,
                    Date = DateTime.Today.Date,
                    Weight = Weight,
                    Version = 0,
                };
                unitOfWork.Reports.Add(newReport);
                await unitOfWork.Complete();

                foreach (var debris in MapDebrisCollection)
                {
                    var newDebris = new DebrisModel()
                    {
                        Latitude = debris.Latitude,
                        Longitude = debris.Longitude,
                        DebrisType = debris.DebrisType,
                        ReportId = newReport.Id,
                    };
                    unitOfWork.Debris.Add(newDebris);
                }
                await unitOfWork.Complete();
            }

            loadingService.Hide();

            await navigationService.GoBackAsync();
        }

        public DelegateCommand CancelReport => _cancelReport ?? (_cancelReport = new DelegateCommand(ExecuteCancelReport));
        private DelegateCommand _cancelReport;
        void ExecuteCancelReport()
            => IsWeightOpen = false;

        #endregion

        #region Navigation

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            trackingService.StopTracking();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            MapDebrisCollection.Clear();
            trackingService.StartTracking(UpdatePosition);
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        #endregion

        #region Helpers

        private void UpdatePosition(Plugin.Geolocator.Abstractions.Position position)
        {
            Latitude = Math.Round(position.Latitude, 6);
            Longitude = Math.Round(position.Longitude, 6);

            MapRotation = position.Heading;
            MapCenter = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude);
        }

        #endregion
    }
}