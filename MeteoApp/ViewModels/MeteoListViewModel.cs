using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MeteoApp.Droid.Models;
using Plugin.Geolocator;

namespace MeteoApp
{
    public class MeteoListViewModel : BaseViewModel
    {
        ObservableCollection<Entry> _entries;
        private OpenWeatherMap WeatherMap;

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
            WeatherMap = new OpenWeatherMap();
            
            

            List<string> cities = new List<string>()
            {
                "Londra",
                "Milano",
                "Napoli",
                "Roma"
            };

            int i = 1;
            foreach (var city in cities)
            {
                var e = new Entry
                {
                    ID = i++,
                    Name = city
                };
                WeatherMap.UpdateWeatherInfo(e);
                Entries.Add(e);
            }

            updateCurrentLocation();
        }
        
        

        private async void updateCurrentLocation()
        {
            var locator = CrossGeolocator.Current;

            // One position
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Entries.Insert(0, new Entry
            {
                ID = 0,
                Name = "Current position",
                Latitude = position.Latitude,
                Longitude = position.Longitude
            });
            
            WeatherMap.UpdateWeatherInfo(Entries.ElementAt(0));
        }
    }
}