using Google.Apis.Calendar.v3.Data;
using Google_Calendar_events_app.DTO;

namespace Google_Calendar_events_app.Converter
{
    public static class GoogleEventConvert
    {
        public static GoogleEventDto GoogleEventConverter(Event calenderEvent)
        {
            return new GoogleEventDto()
            {
                Summary = calenderEvent.Summary,
                Description = calenderEvent.Description,
                End = calenderEvent.End.DateTime.Value,
                Start = calenderEvent.Start.DateTime.Value,
                location = calenderEvent.Location,
                Id = calenderEvent.Id
            };
        }
        public static List<GoogleEventDto> GoogleEventConverter(IList<Event> calenderEvents)
        {
            var allEvent = new List<GoogleEventDto>();
            foreach (var item in calenderEvents)
            {
                allEvent.Add(new GoogleEventDto
                {
                    Summary = item.Summary,
                    Description = item.Description,
                    End = item.End.DateTime.Value,
                    Start = item.Start.DateTime.Value,
                    location = item.Location,
                    Id = item.Id
                });
            }
            return allEvent;
        }
    }

}
