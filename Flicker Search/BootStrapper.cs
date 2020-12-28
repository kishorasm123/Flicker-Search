using Application.Logging;
using Flickr_Search.Views;
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

namespace Flickr_Search
{
    public class BootStrapper : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Registering all the dependency services.
            containerRegistry.RegisterSingleton(typeof(IImageService), typeof(FlickrService));
            containerRegistry.RegisterSingleton(typeof(ILogger), typeof(Logger));
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // Registering all the modules.
            moduleCatalog.AddModule(typeof(SearchBarModuleInit));
            moduleCatalog.AddModule(typeof(StatusBarModuleInit));
            moduleCatalog.AddModule(typeof(SearchResultsModuleInit));
        }
    }
}
