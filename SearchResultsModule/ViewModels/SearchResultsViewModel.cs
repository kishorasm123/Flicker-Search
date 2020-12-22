using Application.Common;
using Application.Common;
using Prism.Commands;
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
        private int currentPageNo = 1;

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

        public DelegateCommand NextCommand { get; set; }

        public DelegateCommand PreviousCommand { get; set; }


        public SearchResultsViewModel(IUnityContainer unityContainer, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.unityContainer = unityContainer;
            this.eventAggregator.GetEvent<ImageSearchEvent>().Subscribe((imageSearchContext) => { Task.Run(() => DoSearch(imageSearchContext.Message)); }, ThreadOption.PublisherThread, false,
                imageSearchContext => { return imageSearchContext.imageSearchContextType == ImageSearchContextType.Request; }
                );

            NextCommand = new DelegateCommand(ExecuteNextCommand, DefaultCanExecuteCommand);
            PreviousCommand = new DelegateCommand(ExecutePreviousCommand, DefaultCanExecuteCommand);
        }

        public async void DoSearch(string searchText, int pageNo = 1)
        {
            try
            {
                // Progress reporting.
                eventAggregator.GetEvent<ImageSearchEvent>().Publish(new ImageSearchContext() { imageSearchContextType = ImageSearchContextType.Response, Message = "Searching for " + searchText + "..." });

                // Creating object of flicker service.
                if (imageService == null) imageService = unityContainer.Resolve<IImageService>();

                // Searching & Fetching the flicker image search results.
                var result = await imageService.ImageSearch(searchText, pageNo);

                dispatcher.Invoke(() => { SearchResult = new ObservableCollection<Image>(result); });

                currentSearchText = searchText;

                // Progress reporting.
                eventAggregator.GetEvent<ImageSearchEvent>().Publish(new ImageSearchContext() { imageSearchContextType = ImageSearchContextType.Response, Message = SearchResult.Count.ToString() + " results found." });
            }
            catch (Exception exception)
            {
                eventAggregator.GetEvent<ImageSearchEvent>().Publish(new ImageSearchContext() { imageSearchContextType = ImageSearchContextType.Response, Message = exception.Message });
            }

        }

        private async void ExecutePreviousCommand()
        {
            if (currentPageNo > 1)
            {
                currentPageNo = currentPageNo - 1;
                DoSearch(currentSearchText, currentPageNo);
            }
        }

        private async void ExecuteNextCommand()
        {
            currentPageNo = currentPageNo + 1;
            DoSearch(currentSearchText, currentPageNo);
        }

        private bool DefaultCanExecuteCommand()
        {
            return true;
        }
    }
}
