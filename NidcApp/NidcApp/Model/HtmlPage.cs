using Newtonsoft.Json;

namespace NidcApp.Model
{
    public class HtmlPage
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("html")]
        public string Html { get; set; }
        [JsonProperty("zoomable")]
        public bool Zoomable { get; set; }
    }
}