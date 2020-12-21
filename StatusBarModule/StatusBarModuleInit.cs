using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using StatusBarModule.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusBarModule
{
    public class StatusBarModuleInit : IModule
    {
        IRegionManager regionManager;
        public void OnInitialized(IContainerProvider containerProvider)
        {
            regionManager.RegisterViewWithRegion("StatusBar", typeof(StatusBar));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Nothing.
        }

        public StatusBarModuleInit(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
    }
}
