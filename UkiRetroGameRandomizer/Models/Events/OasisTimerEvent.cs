namespace UkiRetroGameRandomizer.Models.Events
{
    public class OasisTimerEvent
    {
        public CommandType Type { get; }

        public enum CommandType
        {
            Start, Hide
        }
        
        public OasisTimerEvent(CommandType type)
        {
            Type = type;
        }
    }
}