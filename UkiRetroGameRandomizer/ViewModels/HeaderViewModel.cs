using System.Linq;
using System.Windows;
using Caliburn.Micro;
using UkiRetroGameRandomizer.Models.Enums;
using UkiRetroGameRandomizer.Models.Events;
using UkiRetroGameRandomizer.Models.Factories;

namespace UkiRetroGameRandomizer.ViewModels
{
    public class HeaderViewModel : PropertyChangedBase
        , IHeaderViewModel
        , IHandle<RollStatusChangedEvent>
        , IHandle<GameListLoadedEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _startEnabled;
        private bool _platformsEnabled = true;
        private Visibility _infoVisibility = Visibility.Collapsed;
        private int _listCount;

        public IObservableCollection<IPlatformViewModel> Platforms { get; } =
            new BindableCollection<IPlatformViewModel>();

        public IPlatformViewModel SelectedPlatform { get; set; }

        public bool StartEnabled
        {
            get => _startEnabled;
            set
            {
                _startEnabled = value;
                NotifyOfPropertyChange(() => StartEnabled);
            }
        }

        public bool PlatformsEnabled
        {
            get => _platformsEnabled;
            set
            {
                _platformsEnabled = value;
                NotifyOfPropertyChange(() => PlatformsEnabled);
            }
        }

        public Visibility InfoVisibility
        {
            get => _infoVisibility;
            set
            {
                _infoVisibility = value;
                NotifyOfPropertyChange(() => InfoVisibility);
            }
        }

        public int ListCount
        {
            get => _listCount;
            set
            {
                _listCount = value;
                NotifyOfPropertyChange(() => ListCount);
            }
        }

        public HeaderViewModel(IPlatformViewModelFactory platformViewModelFactory
            , IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            Platforms.AddRange(Models.Data.Platforms.All
                .Select(platformViewModelFactory.Create));
        }

        public void Start()
        {
            _eventAggregator.PublishOnUIThread(new RollStatusChangedEvent(RollStatus.Started));
        }

        public void PlatformChanged()
        {
            StartEnabled = true;
            _eventAggregator.PublishOnUIThread(new PlatformChangedEvent(SelectedPlatform.Platform));
        }

        public void Handle(RollStatusChangedEvent message)
        {
            var enabled = message.Status == RollStatus.Stopped;
            PlatformsEnabled = enabled;
            StartEnabled = enabled;
        }

        public void Handle(GameListLoadedEvent message)
        {
            InfoVisibility = Visibility.Visible;
            ListCount = message.Count;
        }
    }
}