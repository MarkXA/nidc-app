using Newtonsoft.Json;

namespace NidcApp.Model
{
    public class Session
    {
        [JsonProperty("id")]
        public string SessionId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("speaker")]
        public string Speaker { get; set; }
        [JsonProperty("speaker2")]
        public string Speaker2 { get; set; }
        [JsonProperty("lightning")]
        public bool Lightning { get; set; }
        [JsonProperty("time")]
        public string TimeSlotId { get; set; }
        [JsonProperty("room")]
        public string RoomId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("order")]
        public int Order { get; set; }
    }
}