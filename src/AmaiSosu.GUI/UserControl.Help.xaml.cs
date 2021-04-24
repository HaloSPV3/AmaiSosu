using System.Windows.Controls;

namespace AmaiSosu.GUI
{
    /// <summary>
    /// Interaction logic for UserControl.xaml
    /// </summary>
    public partial class UserControlHelp : UserControl
    {
        private Help _help;

        public UserControlHelp()
        {
            InitializeComponent();
            _help = (Help) DataContext;
        }
    }
}
