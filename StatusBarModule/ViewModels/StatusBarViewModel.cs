using Application.Common.Events;
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
            this.eventAggregator.GetEvent<EventProgress>().Subscribe(this.ReportProgress);
        }
        
        private void ReportProgress(string progress)
        {
            ProgressText = progress;
        }
    }
}
