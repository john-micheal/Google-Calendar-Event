namespace Google_Calendar_events_app.DTO
{
    public class CreateGoogleEventDto
    {

        public string Summary { get; set; }

        public string Description { get; set; }

        public string location { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}