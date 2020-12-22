using Application.Common;
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

namespace SearchBarModule.ViewModels
{
    public class SearchBarViewModel : BindableBase
    {
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

        public SearchBarViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            Search = new DelegateCommand(DoSearch, CanSearch).ObservesProperty(() => SearchText);
        }

        private void DoSearch()
        {
            eventAggregator.GetEvent<ImageSearchEvent>().Publish(new ImageSearchContext() { imageSearchContextType = ImageSearchContextType.Request, Message = searchText });
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
