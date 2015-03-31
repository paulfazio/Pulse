using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseApp.Models
{
    public class EventMember : INotifyPropertyChanged, IComparable
    {
        private DateTime lastUpdateTime;
        private string name;
        private double distance;

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

        public DateTime LastUpdateTime
        {
            get
            {
                return this.lastUpdateTime;
            }

            set
            {
                this.lastUpdateTime = value;
                this.NotifyPropertyChanged("LastUpdateTime");
            }
        }

        public double Distance
        {
            get
            {
                return this.distance;
            }

            set
            {
                this.distance = value;
                this.NotifyPropertyChanged("Distance");
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

        public int CompareTo(object other)
        {
            var otherMember = (EventMember)other;

            if (this.Distance < otherMember.Distance)
            {
                return -1;
            }
            else if (this.Distance > otherMember.Distance)
            {
                return 1;
            }
            else
            {
                return this.Name.CompareTo(otherMember.Name);
            }
        }
    }
}
