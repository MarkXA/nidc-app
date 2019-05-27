using System.Collections.Generic;
using System.Linq;
using NidcApp.Models;

namespace NidcApp.ViewModels
{
    public class TimeslotVm
    {
        public string TimeslotId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HasDescription { get; set; }
        public List<SessionVm> Sessions { get; set; }
        public bool HasSessions { get; set; }
        public bool IsPlenary { get; set; }

        public static TimeslotVm FromTimeslot(Timeslot model, Conference conf)
        {
            var result = new TimeslotVm
            {
                TimeslotId = model.id,
                Title = model.title,
                Description = model.description,
                Sessions = conf.sessions.Where(s => !string.IsNullOrWhiteSpace(s.id) && s.time == model.id)
                    .OrderBy(s => s.room)
                    .Select(s => SessionVm.FromSession(s, conf))
                    .ToList(),
                IsPlenary = model.plenary
            };
            result.HasDescription = !string.IsNullOrWhiteSpace(result.Description) && !result.IsPlenary;
            result.HasSessions = result.Sessions.Any();

            return result;
        }
    }
}