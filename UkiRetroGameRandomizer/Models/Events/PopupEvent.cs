namespace UkiRetroGameRandomizer.Models.Events
{
    public class PopupEvent
    {
        public bool Show { get; }
        public string Text { get; }

        public PopupEvent(bool show, string text)
        {
            Show = show;
            Text = text;
        }
    }
}