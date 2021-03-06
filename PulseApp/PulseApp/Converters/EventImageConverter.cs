﻿using PulseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace PulseApp.Converters
{
    public class EventImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var thisEvent = (Event)((Image)value).DataContext;
            var currentEmail = ApplicationData.Current.LocalSettings.Values["CurrentEmail"];
            var thisEventMember = thisEvent.Members.Find(member => member.Email.Equals(currentEmail));

            if (thisEventMember.HasResponded == true)
            {
                return "Assets/CheckMark.png";

            }

            return "Assets/ExclamationMark.png";
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
