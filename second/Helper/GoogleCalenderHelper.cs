using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;
using Google_Calendar_events_app.Helper;
using static Google.Apis.Calendar.v3.EventsResource;
using Google_Calendar_events_app.DTO;
using Google_Calendar_events_app.Converter;

namespace Google_Calendar_events_app.Helper
{
    public class GoogleCalenderHelper
    {
        protected GoogleCalenderHelper()
        {

        }
        public static async Task<GoogleEventDto> CreateGoogleCalendar(CreateGoogleEventDto request)
        {
            var services = createNewService();
            // define request
            Event eventCalendar = new Event()
            {
                Summary = request.Summary,
                Location = request.location,

                Start = new EventDateTime
                {
                    DateTime = request.Start,
                    TimeZone = "Africa/Cairo"
                },
                End = new EventDateTime
                {
                    DateTime = request.End,
                    TimeZone = "Africa/Cairo"
                },
                Description = request.Description
            };

            var eventRequest = services.Events.Insert(eventCalendar, "primary");
            
            var requestCreate = await eventRequest.ExecuteAsync();

            return GoogleEventConvert.GoogleEventConverter(requestCreate);
        }

        public static async Task<List<GoogleEventDto>> GetAllGoogleCalendarEvent()
        {
            var service = createNewService();
            var eventRequest = service.Events.List("primary");
            var events = await eventRequest.ExecuteAsync();
            var allEvent = GoogleEventConvert.GoogleEventConverter(events.Items);

            return allEvent;

        }
        public static async Task<List<GoogleEventDto>> GetCalendareEvent(string searchKeyword)
        {
            var service = createNewService();

            var eventRequest = service.Events.List("primary");
            eventRequest.Q = searchKeyword;
            var events = await eventRequest.ExecuteAsync();
            var allEvent = GoogleEventConvert.GoogleEventConverter(events.Items);

            return allEvent;
        }

        public static async Task<string> RemoveGoogleCalendarEvent(string eventId)
        {
            var service = createNewService();

            var eventRequest = service.Events.Delete("primary", eventId);
            try
            {
                var status = await eventRequest.ExecuteAsync();
                return status;
            }
            catch(Exception  ex)
            {
                return "Event ID may be incorrect ID or event has be deleted before, please ensure form your event id and try again..";
            }

        }

        private static CalendarService createNewService()
        {
            string[] Scopes = { "https://www.googleapis.com/auth/calendar" };

            string ApplicationName = "first project";
            UserCredential credential;
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Cre", "cre.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            // define services
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

   

    }
}