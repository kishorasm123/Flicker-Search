using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SearchResultsModule.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchResultsModule
{
    public class SearchResultsModuleInit : IModule
    {
        IRegionManager regionManager;
        public void OnInitialized(IContainerProvider containerProvier)
        {
            this.regionManager.RegisterViewWithRegion("SearchResults", typeof(SearchResults));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IImageService, FlickrService>();
            // Nothing.
        }

        public SearchResultsModuleInit(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
    }
}
