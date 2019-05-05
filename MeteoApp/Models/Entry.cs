namespace MeteoApp
{
    public class Entry
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public char DN { get; set; }

        public string Description { get; set; }

        public string IconName { get; set; }

        public double TempMin { get; set; }

        public double Temp { get; set; }

        public double TempMax { get; set; }

        public double WindSpeed { get; set; }

        public double Humidity { get; set; }

        

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public void updateInfo()
        {
            DN = IconName[IconName.Length - 1];
        }
    }
}