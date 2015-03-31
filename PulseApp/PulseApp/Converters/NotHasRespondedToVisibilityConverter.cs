using PulseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace PulseApp.Converters
{
    public class NotHasRespondedToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var thisEvent = (Event)value;
            var currentEmail = ApplicationData.Current.LocalSettings.Values["CurrentEmail"];
            var thisEventMember = thisEvent.Members.Find(member => member.Email.Equals(currentEmail));

            return thisEventMember.HasResponded ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
