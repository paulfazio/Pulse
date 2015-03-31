using PulseApp.Common;
using PulseApp.Models;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Email;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace PulseApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewEventPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public NewEventPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EventDetailsPage));
        }

        private async void InviteByEmailButton_Click(object sender, RoutedEventArgs e)
        {
            // Define Recipient
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Name of Recepient",
                Address = "pfazio@microsoft.com"
            };

            // Create email object
            EmailMessage mail = new EmailMessage();
            mail.Subject = "this is the Subject";
            mail.Body = "this is the Body";

            // Add recipients to the mail object
            mail.To.Add(sendTo);
            //mail.Bcc.Add(sendTo);
            //mail.CC.Add(sendTo);

            // Open the share contract with Mail only:
            await EmailManager.ShowComposeNewEmailAsync(mail);

            var dateTime = this.meetingDate.Date.DateTime;
            dateTime = dateTime.AddHours(this.meetingTime.Time.Hours);
            dateTime = dateTime.AddMinutes(this.meetingTime.Time.Minutes);

            var eventMembers = new List<EventMember>()
            {
                new EventMember() { Name = "Avi", Email = "aprince@microsoft.com"},
                new EventMember() { Name = "Nana", Email = "nanaec@microsoft.com"},
                new EventMember() { Name = "Danielle", Email = "danio@microsoft.com"},
                new EventMember() { Name = "Paul", Email = "pfazio@microsoft.com"},
                new EventMember() { Name = "Brandon", Email = "brali@microsoft.com"},
                new EventMember() { Name = "Gilbert", Email = "gilar@microsoft.com"},
            };

            // create Event
            Event pulseEvent = new Event()
            {
                Name = this.meetingName.Text,
                Location = this.meetingLocation.Text,
                Time = dateTime,
                Members = eventMembers
            };

            EventManager eveMgr = new EventManager();
            eveMgr.CreateEvent(pulseEvent, eventMembers);
        }
    }
}
