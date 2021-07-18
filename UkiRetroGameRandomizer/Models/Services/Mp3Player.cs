using System;
using System.IO;
using NAudio.Wave;

namespace UkiRetroGameRandomizer.Models.Services
{
    public class Mp3Player : IDisposable
    {
        private readonly string _rootPath;
        private readonly IWavePlayer _waveOutDevice;
        private WaveStream _mainOutputStream;
        private MemoryStream _ms;

        public Mp3Player(string rootPath)
        {
            _rootPath = rootPath;
            _waveOutDevice = new WaveOut();

            _waveOutDevice.PlaybackStopped += (sender, args) =>
            {
                _ms.Dispose();
                _mainOutputStream.Dispose();
            };
        }

        public void Play(string fileName)
        {
            var fullPath = Path.Combine(_rootPath, fileName);

            _ms = new MemoryStream();

            using (var stream = File.OpenRead(fullPath))
            {
                var buffer = new byte[32768];
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    _ms.Write(buffer, 0, read);
                }
            }

            _ms.Position = 0;
            _mainOutputStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(_ms)));
            _waveOutDevice.Init(_mainOutputStream);
            _waveOutDevice.Play();
        }

        public void Stop()
        {
            _waveOutDevice.Stop();
        }

        public TimeSpan TotalTime(string fileName)
        {
            var fullPath = Path.Combine(_rootPath, fileName);
            using (var fileReader = new Mp3FileReader(fullPath))
            {
                return fileReader.TotalTime;
            }
        }

        public void Dispose()
        {
            _waveOutDevice?.Dispose();
            _mainOutputStream?.Dispose();
            _ms?.Dispose();
        }
    }
}