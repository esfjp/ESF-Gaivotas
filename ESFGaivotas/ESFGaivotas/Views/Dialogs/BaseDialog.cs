using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ESFGaivotas.Views.Dialogs
{
    public class BaseDialog : ContentView
    {
        public BaseDialog()
        {
            IsVisible = false;
        }

        #region Bindable Properties
        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create("IsOpen", typeof(bool), typeof(BaseDialog), false, propertyChanged: Open);
        #endregion

        #region Commanding
        public DelegateCommand CloseDialog => _closeDialog ?? (_closeDialog = new DelegateCommand(ExecuteCloseDialog));
        private DelegateCommand _closeDialog;
        void ExecuteCloseDialog()
        {
            IsOpen = false;
        }
        #endregion

        #region Property Changed Events
        async public static void Open(BindableObject bindable, object oldvalue, object newvalue)
        {
            var controle = (BaseDialog)bindable;
            await controle.Open(controle);
        }
        async Task Open(BaseDialog controle)
        {
            if (IsOpen == true)
            {
                controle.Opacity = 0;
                controle.IsVisible = true;
                await controle.FadeTo(1, 250, Easing.Linear);
            }
            else
            {
                await controle.FadeTo(0, 250, Easing.Linear);
                controle.IsVisible = false;
            }
        }
        #endregion
    }
}
