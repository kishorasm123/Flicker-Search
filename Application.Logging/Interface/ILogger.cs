using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logging
{
    public interface ILogger
    {
        void Open();

        void Log(ImageSearchContext imageSearchContext);

        void Close();

    }
}
