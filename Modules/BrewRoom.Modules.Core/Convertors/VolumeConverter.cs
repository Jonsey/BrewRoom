using System;
using System.Globalization;
using System.Windows.Data;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Convertors
{
    [ValueConversion(typeof(Volume), typeof(String))]
    public class VolumeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var volume = (Volume)value;
            return String.Format("{0} {1}", volume.GetValue(), volume.GetUnit());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Volume((decimal) value, VolumeUnit.Litres);
        }
    }
}
