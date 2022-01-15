using Newtonsoft.Json;

namespace UkiRetroGameRandomizer.Models.Data
{
    public class DroppedGame
    {
        public string Name { get; set; }
        public string Platform { get; set; }
        
        public string Player { get; set; }

        public override string ToString()
        {
            return $"{Name} [{Platform}] ({Player})";
        }
    }
}