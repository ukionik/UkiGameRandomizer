namespace UkiRetroGameRandomizer.Models.Events
{
    public class GameListLoadedEvent
    {
        public int Count { get; }

        public GameListLoadedEvent(int count)
        {
            Count = count;
        }
    }
}