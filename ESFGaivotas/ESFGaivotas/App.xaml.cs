using ESFGaivotas.Controls;
using ESFGaivotas.Services;
using ESFGaivotas.ViewModel;
using ESFGaivotas.Views.Pages;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

namespace ESFGaivotas
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Services
            containerRegistry.RegisterInstance<ITrackingService>(Container.Resolve<TrackingService>());
#if Debug_Mock
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<MockUserService>());
#else
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());
#endif

            // Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
            containerRegistry.RegisterForNavigation<ReportPage, ReportViewModel>();
        }

        public static T Resolve<T>()
        {
            return (Current as App).Container.Resolve<T>();
        }
    }
}
