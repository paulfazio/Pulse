using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace PulseApp.Models
{
    public class Event : INotifyPropertyChanged, IEquatable<Event>
    {
        private string name;
        private string location;
        private DateTime time;
        private List<EventMember> members;
        private List<EventMember> arrivedMembers;
        private List<EventMember> nonarrivedMembers;


        public Guid Id { get; set; }

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

        public string Image
        {
            get
            {
                var currentEmail = ApplicationData.Current.LocalSettings.Values["CurrentEmail"];
                var thisEventMember = this.Members.Find(member => member.Email.Equals(currentEmail));

                if (thisEventMember.HasResponded == true)
                {
                    return "/Assets/CheckMark.png";

                }

                return "/Assets/ExclamationMark.png";
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

        public List<EventMember> ArrivedMembers
        {
            get
            {
                arrivedMembers = this.Members.FindAll(member => member.Distance <= 0.1);
                arrivedMembers.Sort();
                return arrivedMembers;
            }
            set
            {
                arrivedMembers = value;
            }
        }

        public List<EventMember> UnarrivedMembers
        {
            get
            {
                nonarrivedMembers = this.Members.FindAll(member => member.Distance > 0.1);
                nonarrivedMembers.Sort();
                return nonarrivedMembers;
            }

            set
            {
                nonarrivedMembers = value;
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

        public bool Equals(Event other)
        {
            return this.Id.Equals(other.Id);
        }

        public bool Equals(object other)
        {
            var otherEvent = other as Event;

            if (otherEvent == null)
            {
                return false;
            }

            return this.Equals(otherEvent);
        }

        public int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
