using System.Globalization;

namespace AutoOglasiSource.Helpers
{
    public class ByteToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] bytes = value as byte[];
            if (bytes == null)
                return null;

            MemoryStream stream = new MemoryStream(bytes);
            ImageSource imageSource = ImageSource.FromStream(() => stream);

            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
