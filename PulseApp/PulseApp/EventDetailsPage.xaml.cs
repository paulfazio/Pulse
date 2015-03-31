using PulseApp.Common;
using PulseApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
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
    public sealed partial class EventDetailsPage : Page
    {
        private Event currentEvent;
        private NavigationHelper navigationHelper;

        private double myDistance;

        public EventDetailsPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.currentEvent = (Event)e.Parameter;
            this.DataContext = this.currentEvent;
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StatusManager statMgr = new StatusManager();
            //Geopoint currentLocation = mc.GetMyLocation().Result.Coordinate.Point;
            Geopoint currentLocation = await statMgr.GetMyLocation();

            // Fenway location - used instead of local positions
            BasicGeoposition fenwayGeoPos = new BasicGeoposition();
            fenwayGeoPos.Latitude = 42.346711;
            fenwayGeoPos.Longitude = -71.099003;
            Geopoint fenwayGeoPoint = new Geopoint(fenwayGeoPos);

            // show distance for now
            myDistance = CoordinatesHelper.Distance(
                currentLocation.Position.Latitude,
                currentLocation.Position.Longitude,
                fenwayGeoPos.Latitude,
                fenwayGeoPos.Longitude,
                'N');

            // get Event from server
            var pulseEent = EventManager.Instance.GetEventStatus(currentEvent.Id);

            //Update some UI Field with your status
            this.UpdateUIStatus(pulseEent.Members);
        }

        private void UpdateUIStatus(List<EventMember> eventMembers)
        {
            var arrivedMembers = eventMembers.Where(x => x.Distance < 1.0);
            var nonArrivedMembers = eventMembers.Where(x => x.Distance >= 1.0);

            // update arrived
            foreach (var arrivedMem in arrivedMembers)
            {
                if (arrivedMem.Id == SyncEngine.defaultUser.Id)
                {
                    continue;
                }
            }

            // update non arrived
            foreach (var notArrivedMem in nonArrivedMembers)
            {
                if (notArrivedMem.Id == SyncEngine.defaultUser.Id)
                {
                    continue;
                }
            }

            // update my status
        }
    }
}
