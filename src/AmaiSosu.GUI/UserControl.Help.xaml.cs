using System.Windows.Controls;

namespace AmaiSosu.GUI
{
    /// <summary>
    /// Interaction logic for UserControl.xaml
    /// </summary>
    public partial class UserControlHelp : UserControl
    {
        public Help Help { get; set; }

        public UserControlHelp()
        {
            InitializeComponent();
            // DataContext "Help" is assigned in MainWindow and defined 
        }
    }
