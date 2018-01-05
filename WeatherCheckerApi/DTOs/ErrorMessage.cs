namespace WeatherCheckerApi.DTOs
{
    public class ErrorMessage
    {
        public ErrorMessage(string message)
        {
            error = message;
        }

        public string error { get; set; }
    }
}