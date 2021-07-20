using Caliburn.Micro;

namespace UkiRetroGameRandomizer.ViewModels
{
    public class PopupViewModel : Screen, IPopupViewModel
    {
        private string _text;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }
    }
}