using UkiRetroGameRandomizer.Core;
using UkiRetroGameRandomizer.Models.Enums;
using UkiRetroGameRandomizer.ViewModels;

namespace UkiRetroGameRandomizer.Models.Factories
{
    [Factory]
    public interface IGameViewModelFactory
    {
        IGameViewModel Create(GameFontSize fontSize, bool focused);
    }
}