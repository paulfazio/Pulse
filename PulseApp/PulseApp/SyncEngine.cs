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
            members.Add(new EventMember { Distance = 0.5, DisplayName = "Joe Schmoe", Email = "TextBox" });
            members.Add(new EventMember { Distance = 0.1, DisplayName = "Santa Claus", Email = "" });
            members.Add(new EventMember { Distance = 15, DisplayName = "Lebron James", Email = "" });
            members.Add(new EventMember { Distance = 12, DisplayName = "The Muffin Man", Email = "" });
            members.Add(new EventMember { Distance = 8, DisplayName = "John Smith", Email = "" });
            members.Add(new EventMember { Distance = 3, DisplayName = "Paul Fazio", Email = "" });
            members.Add(new EventMember { Distance = 100, DisplayName = "Brandon Li", Email = "" });

            events.Add(new Event { Id = Guid.NewGuid(), Location = "1CC", Name = "Hackathon", Time = DateTime.Now, Members = members });
            events.Add(new Event { Id = Guid.NewGuid(), Location = "1MEM", Name = "Boring stuff", Time = new DateTime(2015, 4, 1, 10, 30, 0), Members = members });

            this.Events.Clear();

            foreach (var nextEvent in events)
            {
                this.Events.Add(nextEvent);
            }
        }
    }
}
