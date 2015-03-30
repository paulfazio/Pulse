using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseApp.Models
{
    public class EventMember : INotifyPropertyChanged
    {
        private bool isAttending;
        private string name;
        private string distance;

        public bool IsAttending
        {
            get
            {
                return this.isAttending;
            }

            set
            {
                this.isAttending = value;
                this.NotifyPropertyChanged("IsAttending");
            }
        }

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

        public string Distance
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
    }
}
