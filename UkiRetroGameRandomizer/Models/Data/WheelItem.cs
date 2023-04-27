namespace UkiRetroGameRandomizer.Models.Data
{
    public class WheelItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Title} ({Type})";
        }
    }
}