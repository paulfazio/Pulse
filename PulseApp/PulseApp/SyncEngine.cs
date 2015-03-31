using PulseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace PulseApp
{
    public class SyncEngine
    {
        private SemaphoreSlim syncSemaphore;
        private DispatcherTimer refreshTimer;
        private static List<Event> currentEvents;

        private static List<Event> events;

        public event EventHandler DataRefreshed;

        public static List<Event> Events { 
            get
            {
                if (currentEvents == null || currentEvents.Count == 0)
                {
                    MockData();
                }

                return currentEvents;
            }
            set
            {
                currentEvents = value;
        }
        }

        public static EventMember defaultUser { get; set; }

        public SyncEngine()
        {
            this.syncSemaphore = new SemaphoreSlim(1);
            this.refreshTimer = new DispatcherTimer { Interval = new TimeSpan(0, 5, 0) };
            Events = new List<Event>();
        }

        public void RefreshData(object sender, object parameter)
        {
            // Refresh the data here

            MockData();

            if (this.DataRefreshed != null)
            {
                this.DataRefreshed(this, EventArgs.Empty);
            }
        }

        public static void MockData()
        {
            var events = new List<Event>();
            var members = new List<EventMember>();
            members.Add(new EventMember { Id = new Guid(), Distance = 0.5, DisplayName = "Joe Schmoe", Email = "joe@outlook.com" });
            members.Add(new EventMember { Id = new Guid(), Distance = 0.1, DisplayName = "Santa Claus", Email = "sc@xmas.com" });
            members.Add(new EventMember { Id = new Guid(), Distance = 15, DisplayName = "Lebron James", Email = "lebron@james.com" });
            members.Add(new EventMember { Id = new Guid(), Distance = 12, DisplayName = "The Muffin Man", Email = "tmm@yahoo.com" });
            members.Add(new EventMember { Id = new Guid(), Distance = 8, DisplayName = "John Smith", Email = "js@gmail.com" });
            members.Add(new EventMember { Id = new Guid(), Distance = 3, DisplayName = "Paul Fazio", Email = "pfazio@microsoft.com" });
            members.Add(new EventMember { Id = new Guid(), Distance = 100, DisplayName = "Brandon Li", Email = "brali@microsoft.com" });
            defaultUser = members[5];

            events.Add(new Event { Id = new Guid(), Location = "1CC", Name = "Hackathon", Time = DateTime.Now, Members = members });
            events.Add(new Event { Id = new Guid(), Location = "1MEM", Name = "Boring stuff", Time = new DateTime(2015, 4, 1, 10, 30, 0), Members = members });

            currentEvents.Clear();

            foreach (var nextEvent in events)
            {
                currentEvents.Add(nextEvent);
            }
        }
    }
}
