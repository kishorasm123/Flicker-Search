using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flickr_Search
{
    interface IWindowCommands
    {
        Action CloseWindow { get; set; }

        Action MinimizeWindow { get; set; }

        Action MaximizeWindow { get; set; }
    }
}
