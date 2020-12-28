using Application.Logging;
using Prism.Commands;
using System;
using Unity;

namespace Flickr_Search.ViewModels
{
    public class ShellViewModel : IWindowCommands
    {
        IUnityContainer unityContainer;
        ILogger logger;

        public DelegateCommand CloseApplication { get; set; }
        public DelegateCommand MinimizeApplication { get; set; }
        public DelegateCommand MaximizeApplication { get; set; }

        public Action CloseWindow { get; set; }
        public Action MinimizeWindow { get; set; }
        public Action MaximizeWindow { get; set; }

        public ShellViewModel(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;

            // Opening logger object.
            logger = unityContainer.Resolve<ILogger>();
            logger.Open();

            // Assigning all the MVVM commands.
            CloseApplication = new DelegateCommand(ExecuteClose);
            MinimizeApplication = new DelegateCommand(ExecuteMinimizeWindow);
            MaximizeApplication = new DelegateCommand(ExecuteMaximizeWindow);
        }

        private void ExecuteMaximizeWindow()
        {
            MaximizeWindow?.Invoke();
        }

        private void ExecuteMinimizeWindow()
        {
            MinimizeWindow.Invoke();
        }

        private void ExecuteClose()
        {
            // Closing the logger and application.
            logger.Close();
            CloseWindow?.Invoke();
        }
    }
}