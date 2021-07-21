using System;

namespace UkiRetroGameRandomizer.Models.Data
{
    public class HistoryLogItem
    {
        public DateTime Date { get; }
        public string PlatformName { get; }
        public string PlatformCaption { get; }
        public string Result { get; }

        public HistoryLogItem(string value)
        {
            var res = value.Split(';');
            Date = DateTime.Parse(res[0]);
            PlatformName = res[1];
            PlatformCaption = res[2];
            Result = res[3];
        }

        public override string ToString()
        {
            return $"[{Date:dd.MM.yy HH:mm:ss}] {Result} ({PlatformCaption})";
        }
    }
}