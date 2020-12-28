using Application.Common;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logging.Class
{
    public class GUILogger : ILogger
    {
        IEventAggregator eventAggregator;

        public GUILogger(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }
        public void Close()
        {
        }

        public void Log(ImageSearchContext imageSearchContext)
        {
            // Progress reporting.
            eventAggregator.GetEvent<ImageSearchEvent>().Publish(imageSearchContext);
        }

        public void Open()
        {
        }
    }
}
