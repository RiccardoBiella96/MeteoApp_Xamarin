using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace MeteoApp.Models
{
    public class OpenWeatherMap
    {
        private static string API_KEY = "d9927f346fc26a1925e207acf647f307";

        public async void UpdateWeatherInfo(Entry entry)
        {
            if(entry.Name.Trim() != "")
            {
                // Define uri
                string Uri;
                if (entry.Name.Equals("Current position"))
                {
                    Uri = "https://api.openweathermap.org/data/2.5/weather?" +
                          "lat=" + entry.Latitude + "&" +
                          "lon=" + entry.Longitude + "&" +
                          "units=metric" + "&" +
                          "APPID=" + API_KEY;
                }
                else
                {
                    Uri = "https://api.openweathermap.org/data/2.5/weather?" +
                          "q=" + entry.Name.Trim() + "&" +
                          "units=metric" + "&" +
                          "APPID=" + API_KEY;
                }

                Debug.WriteLine(Uri);
                // Define client 
                var httpClient = new HttpClient();
                var content = await httpClient.GetStringAsync(Uri);

                Debug.WriteLine(content);

                // Set all properties
                entry.Name = (string)JObject.Parse(content)["name"];
                entry.Description = (string)JObject.Parse(content)["weather"][0]["description"];
                entry.IconName = (string)JObject.Parse(content)["weather"][0]["icon"];
                entry.TempMin = (double)JObject.Parse(content)["main"]["temp_min"];
                entry.Temp = (double)JObject.Parse(content)["main"]["temp"];
                entry.TempMax = (double)JObject.Parse(content)["main"]["temp_max"];
                entry.Humidity = (double)JObject.Parse(content)["main"]["humidity"];
                entry.WindSpeed = (double)JObject.Parse(content)["wind"]["speed"];

                // Update info
                entry.updateInfo();
            }
        }
    }
}