using System.Collections.Generic;

namespace NidcApp.Models
{
    public class Conference
    {
        public string date { get; set; }
        public string lightningIntro { get; set; }
        public IList<Session> sessions { get; set; }
        public IList<Speaker> speakers { get; set; }
        public IList<Room> rooms { get; set; }
        public IList<Timeslot> timeslots { get; set; }
        public IList<Timeslot> lightningslots { get; set; }
        public IList<InformationPage> contentpages { get; set; }
    }
}