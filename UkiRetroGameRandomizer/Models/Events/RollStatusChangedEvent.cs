using UkiRetroGameRandomizer.Models.Enums;

namespace UkiRetroGameRandomizer.Models.Events
{
    public class RollStatusChangedEvent
    {
        public RollStatus Status { get; }

        public RollStatusChangedEvent(RollStatus status)
        {
            Status = status;
        }
    }
}