using System.Windows;
using System.Windows.Media;

namespace UkiRetroGameRandomizer.ViewModels
{
    public interface IGameViewModel
    {
        string Name { get; set; }
        FontWeight FontWeight { get; set; }

        string FocusedColor { get; set; }
        
        string AltColor { get; set; }
    }
}