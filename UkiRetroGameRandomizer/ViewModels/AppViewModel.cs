using System.Windows;
using Caliburn.Micro;
using UkiRetroGameRandomizer.Models.Events;

namespace UkiRetroGameRandomizer.ViewModels
{
    public class AppViewModel : Screen
        , IAppViewModel
    {
        public IHeaderViewModel Header { get; }
        public IGameListViewModel Games { get; }
        
        public IExtraEventViewModel ExtraEvent { get; }
        public IPopupViewModel Popup { get; }

        public AppViewModel(IHeaderViewModel header
        , IGameListViewModel games
        , IPopupViewModel popup
        , IExtraEventViewModel extraEvent)
        {
            Header = header;
            Games = games;
            Popup = popup;
            ExtraEvent = extraEvent;
        }
    }
}