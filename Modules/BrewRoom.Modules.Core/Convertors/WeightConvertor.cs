using System;
using System.Globalization;
using System.Windows.Data;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Convertors
{
    [ValueConversion(typeof(Weight), typeof(String))]
    public class WeightConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var weight = (Weight) value;
            return String.Format("{0} {1}", weight.GetValue(), weight.GetUnit());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String stringValue = value as String;
            var array = stringValue.Split(' ');

            var unit = Enum.Parse(typeof (MassUnit), array[1]);
            return new Weight(System.Convert.ToDecimal(array[0]), (MassUnit)unit);
        }
    }
}
