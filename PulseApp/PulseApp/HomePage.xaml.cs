using PulseApp.Common;
using PulseApp.Models;
using PulseApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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
        private NavigationHelper navigationHelper;


        public HomePage()
        {
            this.InitializeComponent();
            this.DataContext = new HomePageViewModel();
            this.navigationHelper = new NavigationHelper(this);

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        public void EventsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.PromptResponse((Event)((ListView)sender).SelectedItem);

        }


        private async void PromptResponse(Event thisEvent)
        {
            var currentUser = ApplicationData.Current.LocalSettings.Values["CurrentUser"];
            var eventMember = thisEvent.Members.Find(member => member.DisplayName.Equals(currentUser));

            if (eventMember != null && eventMember.HasResponded == false)
            {
                var alert = new MessageDialog("Do you plan on attending this event?");
                alert.Commands.Add(new UICommand("Accept"));
                alert.Commands.Add(new UICommand("Decline"));
                alert.Commands.Add(new UICommand("Cancel"));
                var command = await alert.ShowAsync();

                if (command.Id.Equals("Accept"))
                {
                    this.RespondToInvitation(true);
                    this.ShowEvent(thisEvent);
                }
                else if (command.Id.Equals("Decline"))
                {
                    this.RespondToInvitation(false);
                }

                return;
            }

            this.ShowEvent(thisEvent);
        }

        public void RespondToInvitation(bool isGoing)
        {

        }

        public void ShowEvent(Event thisEvent)
        {
            this.Frame.Navigate(typeof(EventDetailsPage), thisEvent);

        }

        private void ManualRefresh(object sender, RoutedEventArgs e)
        {
            var viewModel = (HomePageViewModel)this.DataContext;
            viewModel.HandleDataRefreshed(this, EventArgs.Empty);
        }
    }
}
