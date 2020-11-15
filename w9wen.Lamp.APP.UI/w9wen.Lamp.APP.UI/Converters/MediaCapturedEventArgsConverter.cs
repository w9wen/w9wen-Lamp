using System;
using System.Globalization;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace w9wen.Lamp.APP.UI.Converters
{
    public class MediaCapturedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mediaCapturedEventArgs = value as MediaCapturedEventArgs;
            if (mediaCapturedEventArgs == null)
            {
                throw new ArgumentException("Expected value to be of type MediaCapturedEventArgs", nameof(value));
            }
            return mediaCapturedEventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}