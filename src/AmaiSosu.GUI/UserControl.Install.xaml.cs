using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AmaiSosu.GUI
{
    /// <summary>
    ///     Interaction logic for InstallUserControl.xaml, <br/>
    ///     a component of MainWindow.xaml
    /// </summary>
    public partial class UserControlInstall : UserControl
    {
        public Install Install { get; set; } = new Install();

        public UserControlInstall()
        {
            InitializeComponent();
            Install = (Install) DataContext;

            if (DataContext == null)
                Install = new Install();
        }

        private async void Commit(object sender, RoutedEventArgs e)
        {
            InstallButton.IsEnabled = false;

            await Task.Run(() => Install.Invoke());

            InstallButton.IsEnabled = true;
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "HCE Executable|haloce.exe"
            };

            if (openFileDialog.ShowDialog() == true)
                Install.Path = Path.GetDirectoryName(openFileDialog.FileName);
        }
    }
}
