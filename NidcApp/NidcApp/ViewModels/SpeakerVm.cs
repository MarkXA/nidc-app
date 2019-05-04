using System.Linq;
using NidcApp.Model;

namespace NidcApp.ViewModels
{
    public class SpeakerVm
    {
        public string SpeakerId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public static SpeakerVm FromSpeaker(Speaker model, Conference conf)
        {
            return new SpeakerVm
            {
                SpeakerId = model.SpeakerId,
                Title = model.Title,
                ImageUrl = $"https://www.nidevconf.com{model.Image}",
                Description = model.Description
            };
        }
    }
}