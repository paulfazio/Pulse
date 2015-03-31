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

        public event EventHandler DataRefreshed;

        public List<Event> Events { get; set; }

        public SyncEngine()
        {
            this.syncSemaphore = new SemaphoreSlim(1);
            this.refreshTimer = new DispatcherTimer { Interval = new TimeSpan(0, 5, 0) };
            this.Events = new List<Event>();
        }

        public void RefreshData(object sender, object parameter)
        {
            // Refresh the data here

            this.MockData();

            if (this.DataRefreshed != null)
            {
                this.DataRefreshed(this, EventArgs.Empty);
            }
        }

        public void MockData()
        {
            var events = new List<Event>();
            var members = new List<EventMember>();
            members.Add(new EventMember { Distance = 0.5, Name = "Joe Schmoe" });
            members.Add(new EventMember { Distance = 0.1, Name = "Santa Claus" });
            members.Add(new EventMember { Distance = 15, Name = "Lebron James" });
            members.Add(new EventMember { Distance = 12, Name = "The Muffin Man" });
            members.Add(new EventMember { Distance = 8, Name = "John Smith" });
            members.Add(new EventMember { Distance = 3, Name = "Paul Fazio" });
            members.Add(new EventMember { Distance = 100, Name = "Brandon Li" });

            events.Add(new Event { Location = "1CC", Name = "Hackathon", Time = DateTime.Now, Members = members });
            events.Add(new Event { Location = "1MEM", Name = "Boring stuff", Time = new DateTime(2015, 4, 1, 10, 30, 0), Members = members });

            this.Events.Clear();

            foreach (var nextEvent in events)
            {
                this.Events.Add(nextEvent);
            }
        }
    }
}
