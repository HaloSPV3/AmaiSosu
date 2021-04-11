using AmaiSosu.GUI.Properties;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AmaiSosu.GUI
{
    public sealed partial class Main
    {
        public class MainInstall : INotifyPropertyChanged
        {
            private Visibility _visibility = Visibility.Collapsed;

            public Visibility Visibility
            {
                get => _visibility;
                set
                {
                    if (value == _visibility) return;
                    _visibility = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
