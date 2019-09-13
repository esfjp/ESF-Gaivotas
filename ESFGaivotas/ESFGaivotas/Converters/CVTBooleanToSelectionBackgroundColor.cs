using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ESFGaivotas.Converters
{
    public class CVTBooleanToSelectionBackgroundColor : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color _selectedBackgroundColor = (Color)Application.Current.Resources["SelectedBackgroundColor"];
            Color _unselectedColor = (Color)Application.Current.Resources["UnselectedColor"];

            var boolean = (bool)value;

            if (boolean) return _selectedBackgroundColor;
            else return _unselectedColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
