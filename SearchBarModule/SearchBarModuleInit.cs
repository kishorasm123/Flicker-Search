using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SearchBarModule.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBarModule
{
    public class SearchBarModuleInit : IModule
    {
        IRegionManager regionManager;
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("SearchBar", typeof(SearchBar));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Nothing.
        }
    }
}
