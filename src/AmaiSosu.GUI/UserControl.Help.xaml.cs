using System.Windows.Controls;

namespace AmaiSosu.GUI
{
    /// <summary>
    /// Interaction logic for UserControl.xaml
    /// </summary>
    public partial class UserControlHelp : UserControl
    {
        /// <summary>
        /// An instance of the Main class pass to this class.
        /// </summary>
        private Main _main = null;

        public UserControlHelp()
        {
            InitializeComponent();
        }

        public Main Main
        {
            set
            {
                _main = value;
            }
        }
    }
}
