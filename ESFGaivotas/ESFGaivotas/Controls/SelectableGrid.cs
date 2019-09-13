using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ESFGaivotas.Controls
{
    public class SelectableFrame : Frame
    {

        #region Bindable Properties

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create("IsSelected", typeof(bool), typeof(SelectableFrame), default(bool), propertyChanged: OnSelectedChanged);

        #endregion

        #region Property Changed Events

        private static void OnSelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SelectableFrame)bindable;
            control.SelectedChangedExecute();
        }
        private void SelectedChangedExecute()
        {
            if (IsSelected) this.ScaleTo(1.0D, 1000, Easing.CubicOut);
            else this.ScaleTo(0.8D, 1000, Easing.CubicOut);
        }

        #endregion

    }
}
