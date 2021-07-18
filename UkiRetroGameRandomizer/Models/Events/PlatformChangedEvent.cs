using UkiRetroGameRandomizer.Models.Data;

namespace UkiRetroGameRandomizer.Models.Events
{
    public class PlatformChangedEvent
    {
        public Platform Platform { get; }

        public PlatformChangedEvent(Platform platform)
        {
            Platform = platform;
        }
    }
}