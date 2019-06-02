using System.Collections.Generic;
using System.Linq;
using NidcApp.Models;
using NidcApp.MxaUi;

namespace NidcApp.ViewModels
{
    public class SessionVm
    {
        public string SessionId { get; set; }
        public Room Room { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<SpeakerVm> Speakers { get; set; }

        public static SessionVm FromSession(Session model, Conference conf, string heading = null)
        {
            return new SessionVm
            {
                SessionId = model.id,
                Room = conf.rooms.FirstOrDefault(r => r.id == model.room),
                Heading = heading,
                Title = model.title,
                Description = model.description,
                Speakers = conf.speakers.Where(s => model.speakers.Contains(s.id))
                    .Select(s => SpeakerVm.FromSpeaker(s, conf))
                    .ToList()
            };
        }
    }
}