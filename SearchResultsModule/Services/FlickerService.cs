using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace SearchResultsModule
{
    public class FlickerService : BindableBase, IImageService
    {
        private Flickr flickr = null;
        private int currentPage = 1;
        private string currentSearchText = string.Empty;

        public void ConnectToFlickerService(string apiKey, string secreteKey)
        {
            if (flickr == null) flickr = new Flickr(Constants.FLICKER_API_KEY, Constants.FLICKER_SECRETE_KEY);
        }

        public async Task<List<Image>> GetImageSearchResults(string searchText, int imagesPerPage)
        {
            try
            {
                if (flickr == null) throw new Exception("Flicker object is null.");

                if (currentSearchText == searchText) currentPage++;
                currentSearchText = searchText;

                PhotoSearchOptions photoSearchOptions = new PhotoSearchOptions() { Tags = searchText, PerPage = imagesPerPage, Page = currentPage };
                PhotoCollection objPhotos = flickr.PhotosSearch(photoSearchOptions);
                List<Image> listOfSearchData = new List<Image>();
                listOfSearchData = objPhotos.Select(i => new Image() { Id = i.PhotoId, Link = i.MediumUrl, Title = i.Title }).ToList<Image>();

                return listOfSearchData;
            }
            catch (Exception exception)
            {
                throw new Exception("Error while performing Flicker search. Error: " + exception.Message);
            }
        }
    }
}
