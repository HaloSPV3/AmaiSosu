using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace AmaiSosu.GUI
{
    /// <summary>
    /// Interaction logic for InstallUserControl.xaml
    /// </summary>
    public partial class UserControlInstall : UserControl
    {
        /// <summary>
        /// An instance of the Main class pass to this class.
        /// </summary>
        private Main _main = null;

        public UserControlInstall()
        {
            InitializeComponent();
        }

        public UserControlInstall(Main main = null)
        {
            InitializeComponent();
            if (_main.Equals(null))
                throw new NullReferenceException("The 'Install' UserControl was started without an instance/DataContext of 'Main'.");
            else _main = main;
        }

        public Main Main
        {
            set
            {
                _main = value;
            }
        }

        private async void Install(object sender, RoutedEventArgs e)
        {
            InstallButton.IsEnabled = false;

            await Task.Run(() => _main.Install.Invoke());

            InstallButton.IsEnabled = true;
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "HCE Executable|haloce.exe"
            };

            if (openFileDialog.ShowDialog() == true)
                _main.Path = Path.GetDirectoryName(openFileDialog.FileName);
        }
    }
}
