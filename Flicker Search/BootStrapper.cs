using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using SearchBarModule;
using SearchResultsModule;
using StatusBarModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Flicker_Search
{

    public class BootStrapper : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton(typeof(IImageService), typeof(FlickerService));
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule(typeof(SearchBarModuleInit));
            moduleCatalog.AddModule(typeof(StatusBarModuleInit));
            moduleCatalog.AddModule(typeof(SearchResultsModuleInit));
        }

    }
}
