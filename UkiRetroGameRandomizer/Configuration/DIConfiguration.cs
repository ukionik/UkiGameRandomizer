using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using UkiHelper;
using UkiRetroGameRandomizer.Core;
using UkiRetroGameRandomizer.Models.Repositories;

namespace UkiRetroGameRandomizer.Configuration
{
    public class DIConfiguration : NinjectModule
    {
        private readonly Assembly _assembly;

        public DIConfiguration(Assembly assembly)
        {
            _assembly = assembly;
        }

        public override void Load()
        {
            BindCore();
            BindRepositories();
            BindViewModels();
            BindFactories();
        }

        private void BindCore()
        {
            Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
        }
        
        private void BindRepositories()
        {
            Bind<IDroppedGameRepository>().To<DroppedGameRepository>().InSingletonScope();
            Bind<IWheelItemRepository>().To<WheelItemRepository>().InSingletonScope();
        }

        private void BindViewModels()
        {
            BindBySuffix("ViewModel", false);
        }

        private void BindBySuffix(string suffix, bool singletone)
        {
            var abstractTypes = _assembly.GetTypes()
                .Where(x => x.IsInterface && x.Name.EndsWith(suffix));

            var instanceTypes = _assembly.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract
                                      && x.Name.EndsWith(suffix))
                .ToList();

            foreach (var abstractType in abstractTypes)
            {
                if (abstractType.GetCustomAttribute<IgnoreAutoBindingAttribute>() != null)
                    continue;

                var modelType = instanceTypes
                    .SingleOrDefault(x => x.GetInterface(abstractType.Name) != null);

                if (modelType != null)
                {
                    var result = Bind(abstractType).To(modelType);

                    if (singletone)
                        result.InSingletonScope();
                }

            }
        }

        private void BindFactories()
        {
            var types = _assembly.GetTypesByCustomAttribute<FactoryAttribute>()
                .ToList();

            foreach (var type in types)
                Bind(type).ToFactory(type);
        }
    }
}