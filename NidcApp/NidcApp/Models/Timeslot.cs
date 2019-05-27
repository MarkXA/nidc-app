namespace NidcApp.Models
{
    public class Timeslot
    {
        public string id { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string title { get; set; }
        public bool allRooms { get; set; }
        public bool plenary { get; set; }
        public string description { get; set; }
    }
}