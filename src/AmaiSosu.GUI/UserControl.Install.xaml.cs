using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace AmaiSosu.GUI
{
    /// <summary>
    ///     Interaction logic for InstallUserControl.xaml, <br/>
    ///     a component of MainWindow.xaml
    /// </summary>
    public partial class UserControlInstall : UserControl
    {
        private Install _install = new Install();

        public UserControlInstall()
        {
            InitializeComponent();
            _install = (Install) DataContext;
        }

        private async void Install(object sender, RoutedEventArgs e)
        {
            InstallButton.IsEnabled = false;

            await Task.Run(() => _install.Invoke());

            InstallButton.IsEnabled = true;
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "HCE Executable|haloce.exe"
            };

            if (openFileDialog.ShowDialog() == true)
                _install.Path = Path.GetDirectoryName(openFileDialog.FileName);
        }
    }
}
