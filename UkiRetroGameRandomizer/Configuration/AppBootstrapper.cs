using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using Ninject;
using UkiRetroGameRandomizer.Models.Data;
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
            var windowSettings = new Dictionary<string, object>
            {
                {"Width", 1000},
                {"Height", 800},
                {"Title", "UkiRetroGameRandomizer 0.5"}
            };

            Platforms.InitPlatforms();
            await _kernel.Get<IDroppedGameRepository>().LoadAsync();
            await _kernel.Get<IWheelItemRepository>().LoadAsync();
            DisplayRootViewFor<IAppViewModel>(windowSettings);
        }
    }

}