using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ESFGaivotas.Converters
{
    public class CVTBooleanTOSelectionBorderColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color _selectedBorderColor = (Color)Application.Current.Resources["SelectedBorderColor"];
            Color _unselectedColor = (Color)Application.Current.Resources["UnselectedColor"];

            var boolean = (bool)value;

            if (boolean) return _selectedBorderColor;
            else return _unselectedColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
