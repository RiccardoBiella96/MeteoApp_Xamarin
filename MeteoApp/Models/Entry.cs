namespace MeteoApp
{
    public class Entry
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string IconName { get; set; }

        public double TempMin { get; set; }

        public double Temp { get; set; }

        public double TempMax { get; set; }

        public double WindSpeed { get; set; }

        public double Humidity { get; set; }

        public string Info { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public void updateInfo()
        {
            Info = "Name: " + Name +
                   "\nLatitude: " + Latitude +
                   "\nLongitude: " + Longitude +
                   "\nDescription: " + Description +
                   "\nIconName: " + IconName +
                   "\nTempMin: " + TempMin +
                   "\nTemp: " + Temp +
                   "\nTempMax: " + TempMax +
                   "\nWindSpeed: " + WindSpeed +
                   "\nHumidity: " + Humidity;
        }
    }
}