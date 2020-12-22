using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace SearchResultsModule
{
    public class FlickrService : IImageService
    {
        //Flicker details
        private const string FLICKER_API_KEY = "6dec9b2f5769cb67a4741f6ee0b0069d";
        private Flickr flickr = null;
        private int currentPage = 1;
        private int imagesPerPage = 10;
        private string currentSearchText = string.Empty;

        private void ConnectToFlickerService()
        {
            try
            {
                if (flickr == null) flickr = new Flickr(FLICKER_API_KEY);
            }
            catch (Exception exception)
            {
                throw new ImageServiceException("Error while creating object for Flicker search. Error: " + exception.Message);
            }
        }

        /// <summary>
        /// This method performs image search operation and restirns the search results.
        /// </summary>
        /// <param name="imageSearchText">This parameter indicates the image search.</param>
        /// <returns>Returns the image search results.</returns>
        /// <exception cref="ImageServiceException">This exeption is thrown when any error occurred while performing search operation.</exception>
        /// <exception cref="ArgumentNullException">This error is thrown if the image serach text is null or whitespaces.</exception>
        public async Task<List<Image>> ImageSearch(string imageSearchText, int pageNo = 0)
        {
            try
            {
                // Throwing argument null exception if image serach text is null of whitespace.
                if (string.IsNullOrWhiteSpace(imageSearchText))
                {
                    throw new ArgumentNullException("Image search text cannot be empty.");
                }

                // Connecting to Flickr service.
                ConnectToFlickerService();

                if (currentSearchText == imageSearchText) currentPage++;
                currentSearchText = imageSearchText;

                currentPage = 1;
                if (pageNo > 0)
                {
                    currentPage = pageNo;
                }

                // Configuring image search options & obtaining the search results.
                PhotoSearchOptions photoSearchOptions = new PhotoSearchOptions() { Tags = imageSearchText, PerPage = imagesPerPage, Page = currentPage };
                PhotoCollection objPhotos = flickr.PhotosSearch(photoSearchOptions);

                // Assigning the search rsults to the custom data structure.
                List<Image> listOfSearchData = new List<Image>();
                listOfSearchData = objPhotos.Select(i => new Image() { Id = i.PhotoId, Link = i.MediumUrl, Title = i.Title }).ToList<Image>();

                return listOfSearchData;
            }
            catch (Exception exception)
            {
                throw new ImageServiceException("Error while performing Flickr search. Error: " + exception.Message);
            }
        }
    }
}
