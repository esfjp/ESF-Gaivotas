using ESFGaivotas.Enumerations;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ESFGaivotas.Converters
{
    public class CVTDebrisTypeTOMediumImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var debrisType = (EDebrisType)value;

            return $"Debris{debrisType.ToString()}Medium.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
