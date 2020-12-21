using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchResultsModule
{
  public  interface IImageService
    {
        void ConnectToFlickerService(string apiKey, string secreteKey);

        Task<List<Image>> GetImageSearchResults(string searchText, int imagesPerPage);
    }
}
