using UkiRetroGameRandomizer.Core;
using UkiRetroGameRandomizer.Models.Data;
using UkiRetroGameRandomizer.ViewModels;

namespace UkiRetroGameRandomizer.Models.Factories
{
    [Factory]
    public interface IPlatformViewModelFactory
    {
        IPlatformViewModel Create(Platform platform);
    }
}