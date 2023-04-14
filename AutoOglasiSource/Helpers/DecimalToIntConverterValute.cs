using System.Globalization;

namespace AutoOglasiSource.Helpers
{
    public class DecimalToIntConverterValute : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is decimal)
            {
                int intValue = (int)(decimal)value;
                string currencySymbol = "€ "; 
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
