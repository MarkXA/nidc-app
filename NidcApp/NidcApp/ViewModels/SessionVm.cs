using System.Collections.Generic;
using System.Linq;
using NidcApp.Model;

namespace NidcApp.ViewModels
{
    public class SessionVm
    {
        public string SessionId { get; set; }
        public Room Room { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<SpeakerVm> Speakers { get; set; }

        public static SessionVm FromSession(Session model, Conference conf)
        {
            return new SessionVm
            {
                SessionId = model.SessionId,
                Room = conf.Rooms.FirstOrDefault(r => r.RoomId == model.RoomId),
                Title = model.Title,
                Description = model.Description,
                Speakers = conf.Speakers.Where(s => s.SpeakerId == model.Speaker || s.SpeakerId == model.Speaker2)
                    .Select(s => SpeakerVm.FromSpeaker(s, conf))
                    .ToList()
            };
        }
    }
}