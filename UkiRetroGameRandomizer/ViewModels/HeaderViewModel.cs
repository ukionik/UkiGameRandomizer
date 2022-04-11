using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Caliburn.Micro;
using UkiHelper;
using UkiRetroGameRandomizer.Configuration;
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
        private readonly IEventAggregator _eventAggregator;
        private bool _startEnabled;
        private bool _platformsEnabled = true;
        private Visibility _infoVisibility = Visibility.Collapsed;
        private Visibility _fabinoVisibility = Visibility.Collapsed;
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
        
        public Visibility FabinoVisibility
        {
            get => _fabinoVisibility;
            set
            {
                _fabinoVisibility = value;
                NotifyOfPropertyChange(() => FabinoVisibility);
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

            var challenge = ConfigurationManager.AppSettings["Challenge"];
            _fabinoVisibility = challenge == "Dropmania3"
                ? Visibility.Visible
                : Visibility.Collapsed;
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

        public void Fabino()
        {
            var selectedPlatformName = SelectedPlatform?.Platform?.Name ?? "";
            
            var dict = new Dictionary<string, string>
            {
                {"NES", "A Boy and His Blob: Trouble on Blobolonia"},
                {"SMD", "Boxing Legends of the Ring"},
                {"SNES", "Bill Laimbeer's Combat Basketball"},
                {"GB", "Bionic Battler"},
                {"GBA", "Barbie and the Magic of Pegasus"},
                {"TG", "Chō Jikū Yōsai Macross: Eien no Love Song"},
                {"SMSGG", "Fantasy Zone: The Maze"},
                {"PS1", "ALIVE"},
                {"N64", "Eiko no Saint Andrews"},
                {"ZX", "Artura"},
                {"MSX", "Aspar GP Master"},
                {"C64", "Alter Ego: Male Version"},
                {"Amiga", "Amiga CD Football"},
                {"DOS", "Adventures of Maddog Williams in the Dungeons of Duridian, The"},
                {"FDS", "Hikari Shinwa: Palthena no Kagami"}
            };


            if (selectedPlatformName.IsNullOrEmpty())
            {
                _eventAggregator.PublishOnUIThread(new PopupEvent(true, "Нужно выбрать платформу"));
            }
            else
            {
                var text = dict.ContainsKey(selectedPlatformName)
                    ? dict[selectedPlatformName]
                    : "Игра не найдена";
                _eventAggregator.PublishOnUIThread(new PopupEvent(true, text));
            }
        }

        public void History()
        {
            var filePath = Path.Combine(AppData.LogPath, "history.log");

            if (File.Exists(filePath))
            {
                var res = File.ReadLines(filePath)
                    .Select(x => new HistoryLogItem(x))
                    .OrderByDescending(x => x.Date)
                    .ToList();
                
                var text = string.Join(Environment.NewLine, res);
                _eventAggregator.PublishOnUIThread(new PopupEvent(true, text));
            }
            else
            {
                _eventAggregator.PublishOnUIThread(new PopupEvent(true, ""));
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