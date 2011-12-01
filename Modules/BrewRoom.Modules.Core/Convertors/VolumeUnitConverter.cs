using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Convertors
{
    public class VolumeUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((VolumeUnit) value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            VolumeUnit result;
            Enum.TryParse(value.ToString(), true, out result);

            return result;
        }
    }
}
