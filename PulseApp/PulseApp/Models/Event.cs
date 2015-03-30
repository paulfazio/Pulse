using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseApp.Models
{
    public class Event : INotifyPropertyChanged
    {
        private string name;
        private string location;
        private DateTime time;
        private List<EventMember> members;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        public string Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
                this.NotifyPropertyChanged("Location");
            }
        }

        public DateTime Time
        {
            get
            {
                return this.time;
            }

            set
            {
                this.time = value;
                this.NotifyPropertyChanged("Time");
            }
        }

        public bool IsCurrentEvent
        {
            get
            {
                return DateTime.Now.Subtract(this.Time) > TimeSpan.FromHours(0.5);
            }
        }

        public List<EventMember> Members
        {
            get
            {
                return this.members;
            }

            set
            {
                this.members = value;
                this.NotifyPropertyChanged("Members");
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
