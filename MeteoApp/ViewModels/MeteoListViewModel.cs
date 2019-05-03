using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace MeteoApp
{
    public class MeteoListViewModel : BaseViewModel
    {
        ObservableCollection<Entry> _entries;

        public ObservableCollection<Entry> Entries
        {
            get { return _entries; }
            set
            {
                _entries = value;
                OnPropertyChanged();
            }
        }

        public MeteoListViewModel()
        {
            Entries = new ObservableCollection<Entry>();

            List<string> cities = new List<string>()
            {
                "Your Position",
                "Londra",
                "Milano",
                "Napoli",
                "Roma"
            };

            int i = 0;
            foreach(var city in cities)
            {
                var e = new Entry
                {
                    ID = i++,
                    Name = city
                };

                Entries.Add(e);
            }
        }
    }
}
