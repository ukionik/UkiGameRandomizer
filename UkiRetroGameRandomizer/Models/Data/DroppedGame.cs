using Newtonsoft.Json;

namespace UkiRetroGameRandomizer.Models.Data
{
    public class DroppedGame
    {
        public string Name { get; set; }
        public string Platform { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Platform})";
        }
    }
}