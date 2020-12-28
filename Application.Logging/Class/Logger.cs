using Application.Common;
using Application.Logging.Class;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Application.Logging
{
    public class Logger : ILogger
    {
        private List<ILogger> loggers = null;
        public Logger(IEventAggregator eventAggregator, IUnityContainer unityContainer)
        {
            loggers = new List<ILogger>();

            loggers.Add(new FileLogger());
            loggers.Add(new GUILogger(eventAggregator));
        }

        public void Close()
        {
            foreach (ILogger logger in loggers)
            {
                logger.Close();
            }
        }

        public void Log(ImageSearchContext imageSearchContex)
        {
            ImageSearchContext logSearchContext = new ImageSearchContext() { SearchText = imageSearchContex.SearchText, Message = imageSearchContex.Message, imageSearchContextType = ImageSearchContextType.Log };

            foreach (ILogger logger in loggers)
            {
                logger.Log(logSearchContext);
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
