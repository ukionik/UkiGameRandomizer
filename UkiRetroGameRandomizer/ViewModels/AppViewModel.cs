using Caliburn.Micro;

namespace UkiRetroGameRandomizer.ViewModels
{
    public class AppViewModel : Screen, IAppViewModel
    {
        public IHeaderViewModel Header { get; }
        public ISiderbarViewModel Siderbar { get; }
        public IGameListViewModel Games { get; }
        
        public IExtraEventViewModel ExtraEvent { get; }

        public AppViewModel(IHeaderViewModel header
        , ISiderbarViewModel siderbar
        , IGameListViewModel games
        , IExtraEventViewModel extraEvent)
        {
            Header = header;
            Siderbar = siderbar;
            Games = games;
            ExtraEvent = extraEvent;
        }
    }
}