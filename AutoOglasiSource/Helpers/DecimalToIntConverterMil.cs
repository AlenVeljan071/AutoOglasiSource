using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoOglasiSource.Helpers
{
    class DecimalToIntConverterMil : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is decimal)
            {
                int intValue = (int)(decimal)value;
                string currencySymbol = "KM ";
                return string.Format("{0}{1}", currencySymbol, intValue);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
