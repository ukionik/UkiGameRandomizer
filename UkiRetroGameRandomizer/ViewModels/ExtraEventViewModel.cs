using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using UkiRetroGameRandomizer.Models.Events;

namespace UkiRetroGameRandomizer.ViewModels
{
    public class ExtraEventViewModel : Screen
        , IExtraEventViewModel
    , IHandle<OasisTimerEvent>
    {
        private readonly Stopwatch _stopwatch = new();
        private const int Interval = 10;
        private static readonly int StartSeconds = 30;
        private int _currentMillis = StartSeconds * 1000;
        private Visibility _visibility = Visibility.Collapsed;
        private readonly DispatcherTimer _timer;

        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                NotifyOfPropertyChange(() => Visibility);
            }
        }

        public string TimerText
        {
            get
            {
                var sec = (_currentMillis / 1000).ToString("00");
                var millis = (_currentMillis % 1000 / 10).ToString("00");

                return _currentMillis > 0
                ? $"0:{sec}.{millis}"
                : "Время вышло!";
            }
        }
        
        public ExtraEventViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
            
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(Interval)
            };

            _timer.Tick += (s, e) => TimerTick();
        }

        private void TimerTick()
        {
            if (_currentMillis > 0)
            {
                _currentMillis = StartSeconds * 1000 - (int)_stopwatch.ElapsedMilliseconds;
                NotifyOfPropertyChange(() => TimerText);
            }
            else
            {
                _currentMillis = 0;
                NotifyOfPropertyChange(() => TimerText);
                _timer.Stop();
                _stopwatch.Stop();
            }
        }
        public void Handle(OasisTimerEvent message)
        {
            if (message.Type == OasisTimerEvent.CommandType.Start)
            {
                Visibility = Visibility.Visible;
                _currentMillis = StartSeconds * 1000;
                _timer.Stop();
                _timer.Start();
                _stopwatch.Restart();                
            }
            else if (message.Type == OasisTimerEvent.CommandType.Hide)
            {
                Visibility = Visibility.Hidden;
                _timer.Stop();
                _stopwatch.Stop();
            }
        }
    }
}