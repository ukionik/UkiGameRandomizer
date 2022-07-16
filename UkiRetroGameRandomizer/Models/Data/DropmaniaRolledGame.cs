using UkiRetroGameRandomizer.Models.Enums;

namespace UkiRetroGameRandomizer.Models.Data
{
    public class DropmaniaRolledGame
    {
        public string Game { get; set; }
        public string Platform { get; set; }
        public string Player { get; set; }
        public DropmaniaGameStatus Status { get; set; }
    }
}