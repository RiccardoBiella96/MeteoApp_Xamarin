using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MeteoApp.Models;
using Plugin.Geolocator;
using System.Net.Http;
using Newtonsoft.Json.Linq;

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

        public void addNewEntry(Entry entry)
        {
            Entries.Add(entry);
        }

        public MeteoListViewModel()
        {
            Entries = new ObservableCollection<Entry>();
            WeatherMap = new OpenWeatherMap();

            // Prelevo dal database le mie città

            getUserLocations();
            updateCurrentLocation();
        }
 
        public async void getUserLocations()
        {
            //Entries.Clear();
            for(int i = 1; i<Entries.Count; i++)
            {
                Entries.RemoveAt(i);
            }

            var httpClient = new HttpClient();
            try
            {
                Console.WriteLine("NON FUNZIONA: " + XAuthKey.xauth);
                if(Models.XAuthKey.xauth == null)
                    Models.XAuthKey.xauth = App.Database.GetItemsWithWhere("token").Result[0].value;
  
                Console.WriteLine("TOKEN LOCATIONS: " + XAuthKey.xauth);
                httpClient.DefaultRequestHeaders.Add("X-Auth", XAuthKey.xauth);

                var result = await httpClient.GetAsync("http://10.11.89.182:8080/protected/getLocations");
                string jsonResult = await result.Content.ReadAsStringAsync();

                string locations = (string)JObject.Parse(jsonResult)["locations"];
                string[] cities = locations.Split(";");

                Console.WriteLine("GET LOCATIONS: " + locations);

                int i = 1;
                foreach (var city in cities)
                {
                    if(city != "")
                    {
                        var e = new Entry
                        {
                            ID = i++,
                            Name = city
                        };
                        WeatherMap.UpdateWeatherInfo(e);
                        Entries.Add(e);
                    }
                }
            }
            catch (Exception e) { }
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