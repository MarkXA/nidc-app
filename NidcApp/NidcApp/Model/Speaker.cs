using Newtonsoft.Json;

namespace NidcApp.Model
{
    public class Speaker
    {
        [JsonProperty("id")]
        public string SpeakerId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("speakerimage")]
        public string Image { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}