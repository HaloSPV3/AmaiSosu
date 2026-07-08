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
        public Install? Install { get; set; }

        public UserControlInstall()
        {
            InitializeComponent();
            // DataContext is assigned in MainWindow
        }

        private async void Commit(object sender, RoutedEventArgs e)
        {
            InstallButton.IsEnabled = false;

            if (Install == null)
            {
                MessageBox.Show($"If you're seeing this, a programmer messed up. {nameof(UserControlInstall)}.{nameof(Install)} is null. So, {nameof(Install)} cannot be invoked.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            await Task.Run(() => Install?.Invoke());

            InstallButton.IsEnabled = true;
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            if (Install == null)
            {
                MessageBox.Show($"If you're seeing this, a programmer messed up. {nameof(UserControlInstall)}.{nameof(Install)} is null. So, {nameof(Install)} cannot process paths.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var openFileDialog = new OpenFileDialog
            {
                Filter = "HCE Executable|haloce.exe"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                Install.Path = Path.GetDirectoryName(openFileDialog.FileName)
                    ?? Path.GetPathRoot(openFileDialog.FileName)
                    ?? string.Empty;
            }
        }
    }
}
