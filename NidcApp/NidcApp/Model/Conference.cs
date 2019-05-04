using System.Collections.Generic;
using Newtonsoft.Json;

namespace NidcApp.Model
{
    public class Conference
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("lightningIntro")]
        public string LightningIntro { get; set; }
        [JsonProperty("sessions")]
        public List<Session> Sessions { get; set; }
        [JsonProperty("speakers")]
        public List<Speaker> Speakers{ get; set; }
        [JsonProperty("rooms")]
        public List<Room> Rooms { get; set; }
        [JsonProperty("timeslots")]
        public List<TimeSlot> TimeSlots { get; set; }
        [JsonProperty("floorplan")]
        public string FloorplanUrl {get; set; }
        [JsonProperty("htmlPages")]
        public List<HtmlPage> HtmlPages { get; set; }
    }
}