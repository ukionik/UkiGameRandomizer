﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using Caliburn.Micro;
using Ninject;
using UkiHelper;
using UkiRetroGameRandomizer.Models.Data;
using UkiRetroGameRandomizer.Models.Enums;
using UkiRetroGameRandomizer.Models.Repositories;
using UkiRetroGameRandomizer.ViewModels;

namespace UkiRetroGameRandomizer.Configuration
{
    public class AppBootstrapper : BootstrapperBase
    {
        private readonly IKernel _kernel;

        public AppBootstrapper()
        {
            Initialize();
            _kernel = DIFactory.CreateNinject();
        }

        protected override void Configure()
        {
        }

        protected override object GetInstance(Type service, string key)
        {
            return _kernel.Get(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _kernel.GetAll(service);
        }

        protected override void BuildUp(object instance)
        {
            _kernel.Inject(instance);
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            var windowWidth = ConfigurationManager.AppSettings["WindowWidth"].ToInt();
            var windowHeight = ConfigurationManager.AppSettings["WindowHeight"].ToInt();

            var windowSettings = new Dictionary<string, object>
            {
                {"Width", windowWidth},
                {"Height", windowHeight},
                {"Title", "UkiRetroGameRandomizer 1.2.5"}
            };

            Platforms.InitPlatforms();

            if (AppData.ProfileEnum == Profile.Dropmania)
            {
                await _kernel.Get<IDroppedGameRepository>().LoadAsync();
                await _kernel.Get<IDropmaniaWheelItemRepository>().LoadAsync();
            }
            else if (AppData.ProfileEnum == Profile.RGG)
            {
                await _kernel.Get<IRggWheelItemRepository>().LoadAsync();
                await _kernel.Get<IDropmaniaRolledGameRepository>().LoadAsync();
            }

            //await _kernel.Get<IRetroPlayPlatformRepository>().LoadAsync();

            DisplayRootViewFor<IAppViewModel>(windowSettings);
        }
    }

}