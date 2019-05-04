using System;
using Newtonsoft.Json;

namespace NidcApp.Model
{
    public class TimeSlot
    {
        [JsonProperty("id")]
        public string TimeSlotId { get; set; }
        [JsonProperty("startTime")]
        public string StartTime { get; set; }
        [JsonProperty("endTime")]
        public string EndTime { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("plenary")]
        public bool Plenary { get; set; }
        [JsonProperty("allRooms")]
        public bool AllRooms { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}