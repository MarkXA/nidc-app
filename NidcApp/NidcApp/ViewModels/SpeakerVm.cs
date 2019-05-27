using NidcApp.Models;

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
                SpeakerId = model.id,
                Title = model.title,
                ImageUrl = model.speakerImage,
                Description = model.description
            };
        }
    }
}