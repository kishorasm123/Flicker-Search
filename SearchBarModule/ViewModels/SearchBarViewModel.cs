using Application.Common;
using Application.Logging;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace SearchBarModule.ViewModels
{
    public class SearchBarViewModel : BindableBase
    {
        ILogger logger;
        IEventAggregator eventAggregator;

        #region Properties

        private string searchText = string.Empty;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                SetProperty(ref searchText, value);
            }
        }

        public DelegateCommand Search { get; set; }

        #endregion Properties

        public SearchBarViewModel(IEventAggregator eventAggregator, IUnityContainer unityContainer)
        {
            this.eventAggregator = eventAggregator;
            logger = unityContainer.Resolve<ILogger>();

            // Assigning all the MVVM commands.
            Search = new DelegateCommand(DoSearch, CanSearch).ObservesProperty(() => SearchText);
        }

        private void DoSearch()
        {
            ImageSearchContext imageSearchContext = new ImageSearchContext() { imageSearchContextType = ImageSearchContextType.Request, SearchText= searchText, Message = "Search for " + searchText + "..." };
            logger.Log(imageSearchContext);

            eventAggregator.GetEvent<ImageSearchEvent>().Publish(imageSearchContext);
        }

        private bool CanSearch()
        {
            // If search text is not null, then returning true.
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                return true;
            }

            // When search text is null, then return false.
            return false;
        }

    }
}
