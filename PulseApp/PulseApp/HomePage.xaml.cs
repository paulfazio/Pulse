﻿using PulseApp.Models;
using PulseApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace PulseApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            this.DataContext = new HomePageViewModel();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var x = App.MobileService.SerializerSettings;
        }

        public void Event_Clicked(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(EventDetailsPage), (Event)e.ClickedItem);

        }

        public void EventsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //this.Frame.Navigate(typeof(EventDetailsPage), ((ListView)sender).SelectedItem);
        }
    }
}
