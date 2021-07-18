using UkiRetroGameRandomizer.Models.Data;

namespace UkiRetroGameRandomizer.ViewModels
{
    public class PlatformViewModel : IPlatformViewModel
    {
        public string Name { get; }
        public Platform Platform { get; }

        public PlatformViewModel(Platform platform)
        {
            Platform = platform;
            Name = platform.Caption;
        }
    }
}