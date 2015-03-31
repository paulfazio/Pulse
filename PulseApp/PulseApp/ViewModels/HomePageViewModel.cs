
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
        private SyncEngine syncEngine;
        private object thisLock;

        public HomePageViewModel()
        {
            this.events = new ObservableCollection<Event>();

            this.thisLock = new Object();
            this.syncEngine = new SyncEngine();
            this.syncEngine.DataRefreshed += this.HandleDataRefreshed;
            this.syncEngine.RefreshData(this, null);
        }

        private ObservableCollection<Event> events;

        public ObservableCollection<Event> Events
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

        public void HandleDataRefreshed(object sender, EventArgs e)
        {
            lock(this.thisLock)
            {
                this.Events.Clear();
                foreach (var nextEvent in SyncEngine.Events)
                {
                    this.Events.Add(nextEvent);
                }
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
