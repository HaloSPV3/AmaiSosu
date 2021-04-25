using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AmaiSosu.GUI
{
    /// <summary>
    /// Interaction logic for CompileUserControl.xaml
    /// </summary>
    public partial class UserControlCompile : UserControl
    {
        public Compile Compile;

        public UserControlCompile()
        {
            InitializeComponent();
            Compile = (Compile) DataContext;
        }

        private async void Commit(object sender, RoutedEventArgs e)
        {
            CompileButton.IsEnabled = false;

            await Task.Run(() => Compile.Invoke());

            CompileButton.IsEnabled = true;
        }

        /// <summary>
        ///     Show an Open File dialog to select a directory containing OpenSauce's binaries.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///     Because the User/Saves files created at a path specified at
        ///     runtime and the "%ProgramData%\\Kornner Studios" is constant,
        ///     only the location of the binaries/libraries is needed.
        /// </remarks>
        private void Browse(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Locate OpenSauce's freshly-built binaries",
                
                ReadOnlyChecked = true,
                Filter = "OpenSauce DLL|*.dll"
            };

            if (openFileDialog.ShowDialog() == true)
                Compile.Path = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
        }
    }
}
