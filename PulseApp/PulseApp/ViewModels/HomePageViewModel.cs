
using System.Collections.Generic;
using PulseApp.Models;
using System.ComponentModel;
using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace PulseApp.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public HomePageViewModel()
        {
            this.events = new List<Event>();

            var members = new List<EventMember>();
            members.Add(new EventMember { Distance = "Very far", IsAttending = true, Name = "Joe Schmoe" });
            this.events.Add(new Event { Location = "1CC", Name = "Hackathon", Time = DateTime.MinValue, Members = members });
            this.events.Add(new Event { Location = "1MEM", Name = "Boring stuff", Time = DateTime.MinValue, Members = members });

        }

        private List<Event> events;
        private List<Event> currentEvent;
        private List<Event> upcomingEvents;
        private List<List<Event>> groupedEvents;

        public class EventGroup : ObservableCollection<Event>
        {
            public string Title { get; set; }

            public EventGroup(IEnumerable<Event> events) : base(events)
            {
            }
        }

        public List<Event> Events
        {
            get
            {
                return this.events;
            }
            set
            {
                this.events = value;
                this.NotifyPropertyChanged("Names");
            }
        }

        public IEnumerable<EventGroup> GroupedEvents
        {
            get
            {
                return from currentEvent in this.Events group currentEvent by currentEvent.IsCurrentEvent into eventGroup select new EventGroup(eventGroup) { Title = eventGroup.Key ? "Current Event" : "Upcoming Events" };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
