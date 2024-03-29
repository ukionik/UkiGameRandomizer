﻿using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using UkiHelper;
using UkiRetroGameRandomizer.Models.Enums;

namespace UkiRetroGameRandomizer.Configuration
{
    public static class AppData
    {
        public static Assembly Assembly { get; }
        public static string AppPath { get; }
        public static string ResourcePath { get; }
        public static string Profile { get; }
        
        public static string ImgPath { get; }

        public static Profile ProfileEnum
        {
            get
            {
                Enum.TryParse(Profile, true, out Profile profile);
                return profile;                
            }
        } 
        public static string GameListPath { get; }
        public static string SoundPath { get; }
        public static string LogPath { get; }
        public static string BackgroundPath { get; }
        
        public static string FontName { get; }
        
        public static string GameRollColor { get; }
        public static string SelectedGameRollColor { get; }
        public static string SelectedItemColor { get; }
        public static string FontBigSize { get; }
        public static string FontMediumSize { get; }
        public static string FontSmallSize { get; }

        static AppData()
        {
            Assembly = typeof(AppData).Assembly;
            AppPath = Assembly.GetExecutablePath();
            ResourcePath = Path.Combine(AppPath, "Resources");
            Profile = ConfigurationManager.AppSettings["Profile"];
            GameListPath = Path.Combine(ResourcePath, "List", Profile);
            var soundSystem = ConfigurationManager.AppSettings["SoundSystem"];
            SoundPath = Path.Combine(ResourcePath, "Sounds", soundSystem);
            LogPath = Path.Combine(AppPath, "Log");
            ImgPath = Path.Combine(ResourcePath, "Img");
            BackgroundPath = Path.Combine(ImgPath, ConfigurationManager.AppSettings["Background"]);
            FontName = ConfigurationManager.AppSettings["FontName"];
            GameRollColor = ConfigurationManager.AppSettings["GameRollColor"];
            SelectedGameRollColor = ConfigurationManager.AppSettings["SelectedGameRollColor"];
            SelectedItemColor = ConfigurationManager.AppSettings["SelectedItemColor"];
            FontBigSize = ConfigurationManager.AppSettings["FontBigSize"];
            FontMediumSize = ConfigurationManager.AppSettings["FontMediumSize"];
            FontSmallSize = ConfigurationManager.AppSettings["FontSmallSize"];
        }
    }
}