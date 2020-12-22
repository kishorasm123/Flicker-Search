using Application.Common;
using Application.Common.Events;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Unity;

namespace SearchResultsModule.ViewModels
{
    public class SearchResultsViewModel : BindableBase
    {
        IEventAggregator eventAggregator;
        IUnityContainer unityContainer;
        IImageService imageService = null;
        Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        private string currentSearchText = string.Empty;

        private ObservableCollection<Image> searchResult = new ObservableCollection<Image>();
        public ObservableCollection<Image> SearchResult
        {
            get
            {
                return searchResult;
            }
            set
            {
                SetProperty(ref searchResult, value);
            }
        }


        public SearchResultsViewModel(IUnityContainer unityContainer, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.unityContainer = unityContainer;
            this.eventAggregator.GetEvent<ImageSearchEvent>().Subscribe((imageSearchContext) => { Task.Run(() => DoSearch(imageSearchContext.Message)); }, ThreadOption.PublisherThread, false,
                imageSearchContext => { return imageSearchContext.imageSearchContextType == ImageSearchContextType.Request; }
                );
        }

        public async void DoSearch(string searchText)
        {
            try
            {
                // Progress reporting.
                eventAggregator.GetEvent<EventProgress>().Publish("Searching for " + searchText + "...");

                // Creating object of flicker service.
                if (imageService == null) imageService = unityContainer.Resolve<IImageService>();

                // Searching & Fetching the flicker image search results.
                var result = await imageService.ImageSearch(searchText);

                // If same item is searched again, then appending the new page result.
                if (currentSearchText == searchText)
                {
                    foreach (Image image in result)
                    {
                        dispatcher.Invoke(() => { SearchResult.Add(image); });
                    }
                }

                // If new item is being searched, then clearing & assigning the search result.
                else
                {
                    dispatcher.Invoke(() => { SearchResult = new ObservableCollection<Image>(result); });
                }

                currentSearchText = searchText;

                // Progress reporting.
                eventAggregator.GetEvent<EventProgress>().Publish(SearchResult.Count.ToString() + " results found.");
            }
            catch (Exception exception)
            {
                eventAggregator.GetEvent<EventProgress>().Publish(exception.Message);
            }

        }

    }
}
