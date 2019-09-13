using ESFGaivotas.Aggregator;
using ESFGaivotas.Model.Core;
using ESFGaivotas.Model.Repository;
using Prism.Commands;
using Prism.Events;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESFGaivotas.Views.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateUserDialog : BaseDialog
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IPageDialogService _pageDialogService;

        public CreateUserDialog()
        {
            InitializeComponent();

            _eventAggregator = App.Resolve<IEventAggregator>();
            _pageDialogService = App.Resolve<IPageDialogService>();
        }

        #region Bindable Properties

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(CreateUserDialog), "Title");

        public string Login
        {
            get { return (string)GetValue(LoginProperty); }
            set { SetValue(LoginProperty, value); }
        }
        public static readonly BindableProperty LoginProperty = BindableProperty.Create("Login", typeof(string), typeof(CreateUserDialog), default(string));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }
        public static readonly BindableProperty PasswordProperty = BindableProperty.Create("Password", typeof(string), typeof(CreateUserDialog), default(string));

        public string FullName
        {
            get { return (string)GetValue(FullNameProperty); }
            set { SetValue(FullNameProperty, value); }
        }
        public static readonly BindableProperty FullNameProperty = BindableProperty.Create("FullName", typeof(string), typeof(CreateUserDialog), default(string));

        #endregion

        #region Commanding

        public DelegateCommand CreateUser => _createUser ?? (_createUser = new DelegateCommand(ExecuteCreateUser));
        private DelegateCommand _createUser;
        async void ExecuteCreateUser()
        {
            if (await ValidateUser())
            {
                using (var unitOfWork = new UnitOfWork(new ESFContext()))
                {
                    var user = new UserModel()
                    {
                        Login = Login,
                        PasswordHash = Password,
                        FullName = FullName,
                        Role = Enumerations.ERole.User,
                        ProfilePicture = "Logo.png",
                    };

                    unitOfWork.User.Add(user);
                    await unitOfWork.Complete();
                    IsOpen = false;
                }
            }
        }

        #endregion

        #region Validation

        private async Task<bool> ValidateUser()
        {
            var passed = true;

            if (string.IsNullOrEmpty(Login))
            {
                await _pageDialogService.DisplayAlertAsync("Alerta", "Você precisa inserir um login!", "OK");
                passed = false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await _pageDialogService.DisplayAlertAsync("Alerta", "Você precisa inserir uma senha!", "OK");
                passed = false;
            }

            if (string.IsNullOrEmpty(FullName))
            {
                await _pageDialogService.DisplayAlertAsync("Alerta", "Você precisa inserir seu nome completo!", "OK");
                passed = false;
            }

            return passed;
        }

        #endregion
    }
}