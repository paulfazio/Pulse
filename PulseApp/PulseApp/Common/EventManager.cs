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
        private static EventManager instance;

        private EventManager() { }

        public static EventManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new EventManager();
                }
                return instance;
            }
        }

        public async void CreateEvent(Event pulseEvent)
        {
            MessageDialog messageDialog = new MessageDialog("Event Creation", "Event has been submitted");
            await messageDialog.ShowAsync();
        }

        public void UpdateEvent(Event pulseEvent)
        {

        }

        public void UpdateMyStatus(Guid eventID, int distance)
        {

        }

        public void GetMyEvents(Guid userID)
        {

        }
        public Event GetEventStatus(Guid eventID)
        {
            return SyncEngine.Events.Where(x => x.Id == eventID).First();
        }
    }
}
