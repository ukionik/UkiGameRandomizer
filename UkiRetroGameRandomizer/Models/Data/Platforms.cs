using System.Collections.Generic;

namespace UkiRetroGameRandomizer.Models.Data
{
    public static class Platforms
    {
        public static readonly Platform NES = new Platform("NES", "NES", "nes.txt");
        public static readonly Platform SMD = new Platform("SMD", "SMD+32X+CD", "smd_32x_cd.txt");
        public static readonly Platform SNES = new Platform("SNES", "SNES", "snes.txt");
        public static readonly Platform GB = new Platform("GB", "GB+GBC", "gb_gbc.txt");
        public static readonly Platform GBA = new Platform("GBA", "GBA", "gba.txt");
        public static readonly Platform TG = new Platform("TG", "TG-16+TurboCD", "tg16_tcd.txt");
        public static readonly Platform SMSGG = new Platform("SMSGG", "SMS+GG", "sms_gg.txt");
        public static readonly Platform PS1 = new Platform("PS1", "Playstation 1", "ps1.txt");
        public static readonly Platform N64 = new Platform("N64", "Nintendo 64", "n64.txt");
        public static readonly Platform ZX = new Platform("ZX", "ZX Spectrum", "zx.txt");
        public static readonly Platform MSX = new Platform("MSX", "MSX+MSX2", "msx_msx2.txt");
        public static readonly Platform C64 = new Platform("C64", "Commodore 64", "c64.txt");
        public static readonly Platform Amiga = new Platform("Amiga", "Amiga", "amg.txt");
        public static readonly Platform DOS = new Platform("DOS", "DOS", "dos.txt");
        public static readonly Platform Wheel = new Platform("Wheel", "Колесо", "wheel.txt");

        private static readonly List<Platform> _all = new List<Platform>();
        public static IEnumerable<Platform> All { get; } = _all;

        static Platforms()
        {
            _all.Add(NES);
            _all.Add(SMD);
            _all.Add(SNES);
            _all.Add(GB);
            _all.Add(GBA);
            _all.Add(TG);
            _all.Add(SMSGG);
            _all.Add(PS1);
            _all.Add(N64);
            _all.Add(ZX);
            _all.Add(MSX);
            _all.Add(C64);
            _all.Add(Amiga);
            _all.Add(DOS);
            _all.Add(Wheel);
        }
    }
}