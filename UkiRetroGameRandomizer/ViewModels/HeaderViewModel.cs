using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Caliburn.Micro;
using UkiHelper;
using UkiRetroGameRandomizer.Models.Data;
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
        private readonly IPopupViewModel _popupViewModel;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private bool _startEnabled;
        private bool _platformsEnabled = true;
        private Visibility _infoVisibility = Visibility.Collapsed;
        private int _listCount;
        private string _letter;

        public IObservableCollection<IPlatformViewModel> Platforms { get; } =
            new BindableCollection<IPlatformViewModel>();

        public IPlatformViewModel SelectedPlatform { get; set; }

        public string Letter
        {
            get => _letter;
            set
            {
                _letter = value;
                NotifyOfPropertyChange(() => Letter);
            }
        }
        
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
            , IPopupViewModel popupViewModel
            , IEventAggregator eventAggregator
            , IWindowManager windowManager)
        {
            _popupViewModel = popupViewModel;
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _eventAggregator.Subscribe(this);
            Platforms.AddRange(Models.Data.Platforms.All
                .Select(platformViewModelFactory.Create));
        }

        public void Start()
        {
            _eventAggregator.PublishOnUIThread(new RollStatusChangedEvent(RollStatus.Started));
        }

        public void RollSettingsChanged()
        {
            if (IsValidRollSettings(SelectedPlatform?.Platform, Letter))
            {
                StartEnabled = true;
                _eventAggregator.PublishOnUIThread(new RollSettingsChangedEvent(SelectedPlatform.Platform, Letter));
            }
            else
            {
                StartEnabled = false;
            }
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
        
        private bool IsValidRollSettings(Platform platform, string str)
        {
            if (platform == null)
            {
                return false;
            }

            if (str.IsNullOrEmpty())
            {
                return true;
            }

            if (str.Length > 1)
            {
                return false;
            }
            
            var reg = new Regex((@"a-zA-Z+"));
            return !reg.Match(str).Success;     
        }
    }
}