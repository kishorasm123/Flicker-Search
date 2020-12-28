using Application.Common;
using Application.Logging;
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
        ILogger logger;
        IEventAggregator eventAggregator;
        IUnityContainer unityContainer;
        IImageService imageService = null;
        Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        private string currentSearchText = string.Empty;

        private bool canPageButtonsVisible;
        public bool CanPageButtonsVisible
        {
            get
            {
                return canPageButtonsVisible;
            }
            set
            {
                SetProperty(ref canPageButtonsVisible, value);
            }
        }

        public string CurrentSearchText
        {
            get
            {
                return currentSearchText;
            }
            set
            {
                SetProperty(ref currentSearchText, value);

                // Updating navigation buttons visibility based on the text.
                if (!string.IsNullOrWhiteSpace(currentSearchText)) CanPageButtonsVisible = true;
                else CanPageButtonsVisible = false;
            }
        }

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

            // Opening logger object.
            logger = unityContainer.Resolve<ILogger>();

            // Subscribing to search event.
            CanPageButtonsVisible = false;
            this.eventAggregator.GetEvent<ImageSearchEvent>().Subscribe((imageSearchContext) => { Task.Run(() => { currentPageNo = 1; DoSearch(imageSearchContext.SearchText); }); }, ThreadOption.PublisherThread, false,
                imageSearchContext => { return imageSearchContext.imageSearchContextType == ImageSearchContextType.Request; }
                );

            // Assigning all the MVVM commands.
            NextCommand = new DelegateCommand(ExecuteNextCommand, DefaultCanExecuteCommand).ObservesProperty(() => CurrentSearchText);
            PreviousCommand = new DelegateCommand(ExecutePreviousCommand, DefaultCanExecuteCommand).ObservesProperty(() => CurrentSearchText);
        }

        public async void DoSearch(string searchText, int pageNo = 1)
        {
            try
            {
                // When search text is empty, then throwing error.
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    throw new ArgumentNullException("Image search text cannot be empty.");
                }

                // Progress reporting.
                logger.Log(new ImageSearchContext() { Message = "Searching for " + searchText + "..." }); ;

                // Creating object of flicker service.
                if (imageService == null) imageService = unityContainer.Resolve<IImageService>();

                // Searching & Fetching the flicker image search results.
                var result = await imageService.ImageSearch(searchText, pageNo);

                // Setting the search result to observable collection for binding.
                dispatcher.Invoke(() => { SearchResult = new ObservableCollection<Image>(result); });

                // Setting the searched text to current search text variable.
                CurrentSearchText = searchText;

                // Progress reporting.
                logger.Log(new ImageSearchContext() { Message = SearchResult.Count.ToString() + " results found for '" + currentSearchText + "'." });
            }
            catch (Exception exception)
            {
                logger.Log(new ImageSearchContext() { Message = exception.Message });
            }
        }

        private async void ExecutePreviousCommand()
        {
            if (currentPageNo > 1)
            {
                logger.Log(new ImageSearchContext() { Message = $"Pressed 'Previous' button. So fetching previous page({currentPageNo}) results for '" + currentSearchText + "'..." }); ;
                currentPageNo = currentPageNo - 1;
                await Task.Run(() => DoSearch(currentSearchText, currentPageNo));
            }
        }

        private async void ExecuteNextCommand()
        {
            logger.Log(new ImageSearchContext() { Message = $"Pressed 'Next' button. So searching more page({currentPageNo}) results for '" + currentSearchText + "'..." }); ;
            currentPageNo = currentPageNo + 1;
            await Task.Run(() => DoSearch(currentSearchText, currentPageNo));
        }

        private bool DefaultCanExecuteCommand()
        {
            if (string.IsNullOrWhiteSpace(currentSearchText))
            {
                return false;
            }
            return true;
        }

    }
}
