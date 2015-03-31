using PulseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace PulseApp.Converters
{
    public class EventImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var thisEvent = (Event)value;
            var currentEmail = ApplicationData.Current.LocalSettings.Values["CurrentEmail"];
            var thisEventMember = thisEvent.Members.Find(member => member.Email.Equals(currentEmail));

            if (thisEventMember.HasResponded == true)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/CheckMark.png"));

            }

            return new BitmapImage(new Uri("ms-appx:///Assets/ExclamationMark.png"));
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
