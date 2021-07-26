using System.Windows;
using Caliburn.Micro;
using UkiRetroGameRandomizer.Models.Events;

namespace UkiRetroGameRandomizer.ViewModels
{
    public class PopupViewModel : Screen
        , IPopupViewModel
    , IHandle<PopupEvent>
    {
        private string _text;
        private Visibility _visibility = Visibility.Collapsed;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                NotifyOfPropertyChange(() => Visibility);
            }
        }

        public PopupViewModel(IEventAggregator eventAggregator)
        {
           eventAggregator.Subscribe(this); 
        }

        public void Close()
        {
            Visibility = Visibility.Collapsed;
        }

        public void Handle(PopupEvent message)
        {
            Visibility = message.Show ? Visibility.Visible : Visibility.Collapsed;
            Text = message.Text;
        }
    }
}