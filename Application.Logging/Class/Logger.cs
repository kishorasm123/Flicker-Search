using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logging
{
    public class Logger : ILogger
    {
        private List<ILogger> loggers = null;
        public Logger()
        {
            loggers = new List<ILogger>();
            loggers.Add(new FileLogger());
        }

        public void Close()
        {
            foreach (ILogger logger in loggers)
            {
                logger.Close();
            }
        }

        public void Log(string information)
        {
            foreach (ILogger logger in loggers)
            {
                logger.Log(information);
            }
        }

        public void Open()
        {
            foreach (ILogger logger in loggers)
            {
                logger.Open();
            }
        }
    }
}
