using System.Collections.Generic;
using System.Linq;
using NidcApp.Model;

namespace NidcApp.ViewModels
{
    public class TimeSlotVm
    {
        public string TimeSlotId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HasDescription { get; set; }
        public List<SessionVm> Sessions { get; set; }
        public bool HasSessions{ get; set; }
        public bool IsPlenary { get; set; }

        public static TimeSlotVm FromTimeSlot(TimeSlot model, Conference conf)
        {
            var result = new TimeSlotVm
            {
                TimeSlotId = model.TimeSlotId,
                Title = model.Title,
                Description = model.Description,
                Sessions = conf.Sessions
                    .Where(s => !string.IsNullOrWhiteSpace(s.TimeSlotId) && s.TimeSlotId == model.TimeSlotId)
                    .OrderBy(s => s.RoomId)
                    .Select(s => SessionVm.FromSession(s, conf))
                    .ToList(),
                IsPlenary = model.Plenary
            };
            result.HasDescription = !string.IsNullOrWhiteSpace(result.Description) && !result.IsPlenary;
            result.HasSessions = result.Sessions.Any();

            return result;
        }
    }
}