using System.Windows;
using Caliburn.Micro;

namespace UkiRetroGameRandomizer.ViewModels
{
    public class ExtraEventViewModel : Screen, IExtraEventViewModel
    {
        private Visibility _visibility;

        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                NotifyOfPropertyChange(() => Visibility);
            }
        }
    }
}