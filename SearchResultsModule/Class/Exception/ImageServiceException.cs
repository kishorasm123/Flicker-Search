using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchResultsModule
{
    internal class ImageServiceException : Exception
    {
        public ImageServiceException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
