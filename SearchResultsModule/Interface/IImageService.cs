using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchResultsModule
{
    public interface IImageService
    {
        /// <summary>
        /// This method performs image search operation and restirns the search results.
        /// </summary>
        /// <param name="imageSearchText">This parameter indicates the image search.</param>
        /// <returns>Returns the image search results.</returns>
        /// <exception cref="ImageServiceException">This exeption is thrown when any error occurred while performing search operation.</exception>
        /// <exception cref="ArgumentNullException">This error is thrown if the image serach text is null or whitespaces.</exception>
        Task<List<Image>> ImageSearch(string imageSearchText);
    }
}
