using System.Windows.Controls;

namespace AmaiSosu.GUI
{
    /// <summary>
    /// Interaction logic for UserControl.xaml
    /// </summary>
    public partial class UserControlHelp : UserControl
    {
        public Help Help { get; set; } = new Help();

        public UserControlHelp()
        {
            InitializeComponent();
            Help = (Help) DataContext;
        }
    }
}
