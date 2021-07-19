using UkiRetroGameRandomizer.Models.Data;

namespace UkiRetroGameRandomizer.Models.Events
{
    public class RollSettingsChangedEvent
    {
        public Platform Platform { get; }
        public string Letter { get; }

        public RollSettingsChangedEvent(Platform platform, string letter)
        {
            Platform = platform;
            Letter = letter;
        }
    }
}