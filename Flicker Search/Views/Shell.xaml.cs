using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flickr_Search.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
            Loaded += Shell_Loaded;
        }

        private void Shell_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is IWindowCommands windowCommands)
            {
                windowCommands.CloseWindow += () =>
                 {
                     SystemCommands.CloseWindow(this);
                 };

                windowCommands.MinimizeWindow += () =>
                {
                    SystemCommands.MinimizeWindow(this);
                };

                windowCommands.MaximizeWindow += () =>
                {
                    SystemCommands.MaximizeWindow(this);
                };
            }
        }
    }
}
