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

        void Log(string information);

        void Close();

    }
}
