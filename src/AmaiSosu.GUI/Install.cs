using AmaiSosu.GUI.Properties;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AmaiSosu.GUI.Resources;
using System.IO;
using System;

namespace AmaiSosu.GUI
{
    public class Install : INotifyPropertyChanged
    {
        /// <summary>
        ///     Installation is possible given the current state.
        /// </summary>
        private bool _canInstall;

        private string _path = Startup.Path;

        /// <summary>
        ///     Current state of the OpenSauce installation.
        /// </summary>
        private string _installState = Messages.BrowseHce;

        private Visibility _visibility = Visibility.Collapsed;

        /// <summary>
        ///     Installation is possible given the current state.
        /// </summary>
        public bool CanInstall
        {
            get => _canInstall;
            set
            {
                if (value == _canInstall) return;
                _canInstall = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Current state of the OpenSauce installation.
        /// </summary>
        public string InstallText
        {
            get => _installState;
            set
            {
                if (value == _installState) return;
                _installState = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Installation path.
        /// </summary>
        public string Path
        {
            get => _path;
            set
            {
                if (value == _path) return;
                _path = value;
                OnPathChanged();
            }
        }

        /// <summary>
        ///     Invokes the installation procedure.
        /// </summary>
        public void Invoke()
        {
            try
            {
                InstallText = "...";
                new AmaiSosu.Main(Path).Install();
                InstallText = Messages.InstallSuccess;
            }
            catch (Exception e)
            {
                InstallText = e.Message;
            }
        }

        /// <summary>
        ///     Updates the install text upon successful path provision.
        /// </summary>
        private void OnPathChanged()
        {
            CanInstall = Directory.Exists(Path) && File.Exists(System.IO.Path.Combine(Path, FileNames.HceExecutable));

            InstallText = CanInstall
                ? Messages.InstallReady
                : Messages.BrowseHce;
        }

        /// <summary>
        /// Visibility property of the Install UserControl
        /// </summary>
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
