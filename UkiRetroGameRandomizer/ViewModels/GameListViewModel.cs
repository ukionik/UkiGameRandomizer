using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using GameRandomizerEngine;
using UkiRetroGameRandomizer.Configuration;
using UkiRetroGameRandomizer.Models.Data;
using UkiRetroGameRandomizer.Models.Enums;
using UkiRetroGameRandomizer.Models.Events;
using UkiRetroGameRandomizer.Models.Factories;
using UkiRetroGameRandomizer.Models.Services;

namespace UkiRetroGameRandomizer.ViewModels
{
    public class GameListViewModel : Screen
        , IGameListViewModel
        , IHandle<RollStatusChangedEvent>
        , IHandle<RollSettingsChangedEvent>
    {
        private const int Interval = 10;
        private int _dueTime;
        private readonly Random _random = new Random(DateTime.Now.Millisecond);

        private GameRandomizer _randomizer;
        private bool _started;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly DispatcherTimer _timer;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPopupViewModel _popupViewModel;
        private readonly IWindowManager _windowManager;
        private readonly Mp3Player _mp3Player;

        private IGameViewModel _previousGame2;
        private IGameViewModel _previousGame1;
        private IGameViewModel _currentGame;
        private IGameViewModel _nextGame1;
        private IGameViewModel _nextGame2;
        private Platform _platform;
        private string _letter;
        private IEnumerable<GameInfo> _games;

        public IGameViewModel PreviousGame2
        {
            get => _previousGame2;
            set
            {
                _previousGame2 = value;
                NotifyOfPropertyChange(() => PreviousGame2);
            }
        }

        public IGameViewModel PreviousGame1
        {
            get => _previousGame1;
            set
            {
                _previousGame1 = value;
                NotifyOfPropertyChange(() => PreviousGame1);
            }
        }

        public IGameViewModel CurrentGame
        {
            get => _currentGame;
            set
            {
                _currentGame = value;
                NotifyOfPropertyChange(() => CurrentGame);
            }
        }

        public IGameViewModel NextGame1
        {
            get => _nextGame1;
            set
            {
                _nextGame1 = value;
                NotifyOfPropertyChange(() => NextGame1);
            }
        }

        public IGameViewModel NextGame2
        {
            get => _nextGame2;
            set
            {
                _nextGame2 = value;
                NotifyOfPropertyChange(() => NextGame2);
            }
        }

        public bool Started
        {
            get => _started;
            set
            {
                _started = value;
                NotifyOfPropertyChange(() => Started);
            }
        }

        public GameListViewModel(IEventAggregator eventAggregator
            , IGameViewModelFactory gameViewModelFactory
            , IPopupViewModel popupViewModel
            , IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _popupViewModel = popupViewModel;
            _windowManager = windowManager;
            _eventAggregator.Subscribe(this);
            PreviousGame2 = gameViewModelFactory.Create(GameFontSize.Small, false);
            PreviousGame1 = gameViewModelFactory.Create(GameFontSize.Medium, false);
            CurrentGame = gameViewModelFactory.Create(GameFontSize.Big, true);
            NextGame1 = gameViewModelFactory.Create(GameFontSize.Medium, false);
            NextGame2 = gameViewModelFactory.Create(GameFontSize.Small, false);

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(Interval)
            };

            _timer.Tick += (s, e) => TimerTick();
            _mp3Player = new Mp3Player(AppData.SoundPath);
        }

        private void Start()
        {
            var fileName = GetRandomFile(Path.Combine(AppData.SoundPath, "Roll"));

            var length = _mp3Player.TotalTime(fileName);
            _dueTime = (int) length.TotalMilliseconds;

            if (_dueTime > 120_000)
            {
                _dueTime = 120_000;
            }
            else if (_dueTime < 10_000)
            {
                _dueTime = 10_000;
            }

            _dueTime = 1_000;

            var delta = _random.Next(1000, 3000);
            _dueTime += delta;

            InitGames(_platform, _letter);
            _timer.Stop();
            _timer.Start();
            _stopwatch.Restart();
            Started = true;
            CurrentGame.FontWeight = FontWeights.Normal;
            _mp3Player.Play(fileName);
        }

        private void InitGames(Platform platform, string letter)
        {
            var fileName = Path.Combine(AppData.GameListPath, platform.FileName);
            _games = File.ReadAllLines(fileName)
                .Select(x => new GameInfo(x))
                .Where(x => string.IsNullOrEmpty(letter) || x.Name.StartsWith(letter.ToUpper()))
                .Select(x => x);
            _randomizer = new GameRandomizer(_games, _dueTime, DateTime.Now.Millisecond);
        }

        private void UpdateGameList(int tick)
        {
            var gameInfo = _randomizer.GetGame(tick);

            var previousGame2 = _randomizer.Games.GetPreviousGame2(gameInfo);
            var previousGame = _randomizer.Games.GetPreviousGame(gameInfo);
            var nextGame = _randomizer.Games.GetNextGame(gameInfo);
            var nextGame2 = _randomizer.Games.GetNextGame2(gameInfo);

            PreviousGame2.Name = previousGame2.Name;
            PreviousGame1.Name = previousGame.Name;
            CurrentGame.Name = gameInfo.Name;
            NextGame1.Name = nextGame.Name;
            NextGame2.Name = nextGame2.Name;
        }

        private void TimerTick()
        {
            if (Started && _stopwatch.ElapsedMilliseconds < _dueTime)
            {
                UpdateGameList((int) _stopwatch.ElapsedMilliseconds);
            }
            else
            {
                _timer.Stop();
                _stopwatch.Stop();
                GameRollFinished();
            }
        }

        public void Handle(RollStatusChangedEvent message)
        {
            if (message.Status == RollStatus.Started)
            {
                Start();
            }
        }

        public void Handle(RollSettingsChangedEvent message)
        {
            _platform = message.Platform;
            _letter = message.Letter;
            InitGames(message.Platform, message.Letter);
            _eventAggregator.PublishOnUIThread(new GameListLoadedEvent(_games.Count()));
        }

        private void GameRollFinished()
        {
            Started = false;
            CurrentGame.FontWeight = FontWeights.Bold;

            CurrentGame.Name = "Живительный оазис";

            _mp3Player.Play(GetRandomFile(Path.Combine(AppData.SoundPath, "Fanfare")));
            HistoryLogger.Log(_platform.Name, _platform.Caption, CurrentGame.Name);
            _eventAggregator.PublishOnUIThread(new RollStatusChangedEvent(RollStatus.Stopped));
        }

        public void Navigate()
        {
            if (!Started)
            {
                if (_platform.Name.Equals("wheel", StringComparison.OrdinalIgnoreCase)
                    || _platform.Name.Equals("items", StringComparison.OrdinalIgnoreCase))
                {
                    if (_platform.Name.Equals("wheel", StringComparison.OrdinalIgnoreCase))
                    {
                        _popupViewModel.Text = FindWheelDescription();
                    }
                    
                    if (_platform.Name.Equals("items", StringComparison.OrdinalIgnoreCase))
                    {
                        _popupViewModel.Text = FindItemsDescription();
                    }

                    var windowSettings = new Dictionary<string, object>
                    {
                        {"Width", 640},
                        {"Height", 480},
                        {"Title", CurrentGame.Name}
                    };

                    _windowManager.ShowWindow(_popupViewModel, settings: windowSettings);
                }
                else
                {
                    var query = CurrentGame.Name.Trim().Replace(" ", "+");
                    Process.Start($"http://gamefaqs.com/search?game={query}");
                    Process.Start($"http://youtube.com/results?search_query={query}+longplay");
                }
            }
        }
        private string FindWheelDescription()
        {
            var filePath = Path.Combine(AppData.GameListPath, "Wheel", $"{CurrentGame.Name}.txt");
            bool exists = File.Exists(filePath); 
            return File.Exists(filePath) ? string.Join(Environment.NewLine, File.ReadAllLines(filePath)) : "";
        }
        
        private string FindItemsDescription()
        {
            var filePath = Path.Combine(AppData.GameListPath, "Items", $"{CurrentGame.Name}.txt");
            return File.Exists(filePath) ? string.Join(Environment.NewLine, File.ReadAllLines(filePath)) : "";
        }
        private string GetRandomFile(string path)
        {
            var files = Directory.GetFiles(path);

            var index = _random.Next(0, files.Length);
            return files[index];
        }
    }
}