using System.Windows;

namespace UkiRetroGameRandomizer.ViewModels
{
    public interface IGameViewModel
    {
        string Name { get; set; }
        FontWeight FontWeight { get; set; }
    }
}