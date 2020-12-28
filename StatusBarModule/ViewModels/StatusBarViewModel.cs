using Application.Common;
using Application.Common;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusBarModule.ViewModels
{
    public class StatusBarViewModel : BindableBase
    {
        IEventAggregator eventAggregator;

        private string progressText = string.Empty;
        public string ProgressText
        {
            get
            {
                return progressText;
            }
            set
            {
                SetProperty(ref progressText, value);
            }
        }

        public StatusBarViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            // Subscribing to events.
            this.eventAggregator.GetEvent<ImageSearchEvent>().Subscribe((imageSearchContext) => { ReportProgress(imageSearchContext.Message); }, ThreadOption.PublisherThread, false,
                imageSearchContext =>
                {
                    return imageSearchContext.imageSearchContextType == ImageSearchContextType.Log;
                });
        }
        private void ReportProgress(string progress)
        {
            ProgressText = progress;
        }
    }
}
