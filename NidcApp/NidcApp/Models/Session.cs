using System.Collections.Generic;

namespace NidcApp.Models
{
    public class Session
    {
        public bool draft { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public IList<string> speakers { get; set; }
        public bool keynote { get; set; }
        public bool lightning { get; set; }
        public int order { get; set; }
        public string time { get; set; }
        public string room { get; set; }
        public string slides { get; set; }
        public string video { get; set; }
        public string description { get; set; }
    }
}