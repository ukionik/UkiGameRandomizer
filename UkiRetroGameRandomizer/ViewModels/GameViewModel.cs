using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using UkiRetroGameRandomizer.Configuration;
using UkiRetroGameRandomizer.Core;
using UkiRetroGameRandomizer.Models.Enums;

namespace UkiRetroGameRandomizer.ViewModels
{
    public class GameViewModel : PropertyChangedBase, IGameViewModel
    {
        private string _name = "----------";
        private FontWeight _fontWeight = FontWeights.Normal;
        private string _altColor;
        private readonly string _defaultForegroundColor;
        private string _fontFamily;

        public string FontFamily
        {
            get => _fontFamily;
            set
            {
                _fontFamily = value;
                NotifyOfPropertyChange(() => FontFamily);
            }
        }

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

        public string FocusedColor { get; set; }

        public string AltColor
        {
            get => _altColor;
            set
            {
                _altColor = value;

                if (_altColor != null)
                {
                    Foreground = new SolidColorBrush((Color) ColorConverter.ConvertFromString(_altColor));
                    NotifyOfPropertyChange(() => Foreground);
                }
                else
                {
                    Foreground = new SolidColorBrush((Color) ColorConverter.ConvertFromString(_defaultForegroundColor));
                    NotifyOfPropertyChange(() => Foreground);
                }
            }
        }

        public int FontSize { get; }
        public Brush Foreground { get; set; } = new SolidColorBrush(Colors.White);
        public Thickness Margin { get; } = new Thickness(0, 20, 0, 20);

        public GameViewModel(GameFontSize fontSize, bool focused)
        {
            _fontFamily = AppData.FontName;
            _defaultForegroundColor = focused ? AppData.SelectedGameRollColor : AppData.GameRollColor;
            FontSize = fontSize.ToRealFontSize();
        }
    }
}