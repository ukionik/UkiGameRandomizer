using UkiRetroGameRandomizer.Models.Enums;

namespace UkiRetroGameRandomizer.Core
{
    public static class Extensions
    {
        public static int ToRealFontSize(this GameFontSize fontSize)
        {
            switch (fontSize)
            {
                case GameFontSize.Small:
                    return 24;
                case GameFontSize.Medium:
                    return 36;
                case GameFontSize.Big:
                    return 48;
                default:
                    return 0;
            }
        }

    }
}