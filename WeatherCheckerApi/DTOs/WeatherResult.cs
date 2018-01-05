namespace WeatherCheckerApi.DTOs
{
    public class WeatherResult
    {
        public WeatherResult(string country, string city, double temperature, double humidity)
        {
            this.location = new Location() {city = city, country = country};
            this.temperature = new Temperature() {value = temperature, format = "Celsius"};
            this.humidity = humidity;
        }

        public Location location { get; set; }

        public Temperature temperature { get; set; }

        public double humidity { get; set; }
    }

    public class Location
    {
        public string city { get; set; }

        public string country { get; set; }
    }

    public class Temperature
    {
        public string format { get; set; }
        public double value { get; set; }
    }
}