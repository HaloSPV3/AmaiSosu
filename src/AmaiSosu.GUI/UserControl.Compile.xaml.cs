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
        /// <summary>
        /// An instance of the Main class pass to this class.
        /// </summary>
        private Main _main;
        private Compile _compile;

        public UserControlCompile()
        {
            InitializeComponent();
            if (!_main.Equals(null))
                return;
            else
            {
                var msg = $"{Name} was started without an instanced DataContext of {_main.GetType().Name}.";
                throw new NullReferenceException(msg);
            }
        }

        public UserControlCompile(Main main = null)
        {
            InitializeComponent();
            if (!_main.Equals(null))
                _main = main;
            else
            {
                var msg = $"{Name} was started without an instanced DataContext of {_main.GetType().Name}.";
                throw new NullReferenceException(msg);
            }

            _compile = (Compile) DataContext;
            _compile.Initialise();
        }

        private async void Compile(object sender, RoutedEventArgs e)
        {
            CompileButton.IsEnabled = false;

            await Task.Run(() => _main.Compile.Invoke());

            CompileButton.IsEnabled = true;
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Locate OpenSauce's freshly-built binaries",
                
                ReadOnlyChecked = true,
                Filter = "OpenSauce DLL|*.dll"
            };

            if (openFileDialog.ShowDialog() == true)
                _compile.Path = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
        }
    }
}
