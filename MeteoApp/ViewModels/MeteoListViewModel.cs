using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Plugin.Geolocator;

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

            for (var i = 1; i < 10; i++)
            {
                var e = new Entry
                {
                    ID = i,
                    Name = "Entry " + i,
                    Latitude = 0,
                    Longitude = 0
                };

                Entries.Add(e);
            }

            Entries.Add(GetCurrentLocation().Result);
        }

        private async Task<Entry> GetCurrentLocation()
        {
            var locator = CrossGeolocator.Current;

            // One position
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            return new Entry
            {
                ID = 0,
                Name = "Entry " + 0 + " Latitude: " + position.Latitude + " Longitude " + position.Longitude,
                Latitude = position.Latitude,
                Longitude = position.Longitude
            };
        }
    }
}