using PulseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PulseApp.Common
{
    public class EventManager
    {
        public async void CreateEvent(Event pulseEvent, List<EventMember> intendedAttendees)
        {
            MessageDialog messageDialog = new MessageDialog("Event Creation", "Event has been submitted");
            await messageDialog.ShowAsync();
        }

        public void UpdateEvent(Event pulseEvent, List<EventMember> intendedAttendees)
        {

        }

        public void UpdateMyStatus(Guid eventID, int distance)
        {

        }

        public void GetMyEvents(Guid userID)
        {

        }
        public void GetEventStatus(Guid eventID)
        {

        }
    }
}
