using System.Collections.Generic;

namespace NidcApp.Models
{
    public class Speaker
    {
        public bool draft { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public bool? keynote { get; set; }
        public string speakerImage { get; set; }
        public IList<string> links { get; set; }
        public string description { get; set; }
    }
}