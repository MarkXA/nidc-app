using Newtonsoft.Json;

namespace NidcApp.Model
{
    public class Room
    {
        [JsonProperty("id")]
        public string RoomId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("abbreviation")]
        public string Abbreviation { get; set; }
    }
}