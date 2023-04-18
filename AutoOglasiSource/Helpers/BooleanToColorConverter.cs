using System.Globalization;

namespace AutoOglasiSource.Helpers
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
            {
                return Color.FromRgb(255, 0, 0);
            }
            else
            {
                return Color.FromRgb(255, 255, 255);
            }

          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

      
    }
}
