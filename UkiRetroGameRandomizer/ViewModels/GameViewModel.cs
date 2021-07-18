using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using UkiRetroGameRandomizer.Core;
using UkiRetroGameRandomizer.Models.Enums;

namespace UkiRetroGameRandomizer.ViewModels
{
    public class GameViewModel : PropertyChangedBase, IGameViewModel
    {
        private string _name = "----------";
        private FontWeight _fontWeight = FontWeights.Normal;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        
        public FontWeight FontWeight
        {
            get => _fontWeight;
            set
            {
                _fontWeight = value;
                NotifyOfPropertyChange(() => FontWeight);
            }
        }

        public int FontSize { get; }
        public Brush Foreground { get; } = new SolidColorBrush(Colors.White);
        public Thickness Margin { get; } = new Thickness(0, 20, 0 ,20);

        public GameViewModel(GameFontSize fontSize, bool focused)
        {
            FontSize = fontSize.ToRealFontSize();

            if (focused)
            {
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BE9A21"));
            }
        }
    }
}