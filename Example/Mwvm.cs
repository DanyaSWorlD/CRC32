using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Daquga.Security.Cryptography;

using Example.Annotations;

namespace Example
{
    public class Mwvm : INotifyPropertyChanged
    {
        private string _stringInput;
        private string _fileInput;
        private string _stringHash;
        private string _fileHash;
        private string _fileName;

        public event PropertyChangedEventHandler PropertyChanged;

        public string StringInput
        {
            get => _stringInput;
            set
            {
                _stringInput = value;
                OnPropertyChanged();
            }
        }

        public string FileInput
        {
            get => _fileInput;
            set
            {
                _fileInput = value;
                OnPropertyChanged();
            }
        }

        public string StringHash
        {
            get => _stringHash;
            set
            {
                _stringHash = value;
                OnPropertyChanged();
            }
        }

        public string FileHash
        {
            get => _fileHash;
            set
            {
                _fileHash = value;
                OnPropertyChanged();
            }
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged();
            }
        }

        public ICommand CalculateStringHashCommand => new CommandHandler(
            () =>
                {
                    StringHash = CRC32.FromString(StringInput);
                });

        public ICommand CalculateFileHashCommand => new CommandHandler(
            () =>
                {
                    FileHash = CRC32.FromFile(FileInput);
                });

        public ICommand SelectFile => new CommandHandler(
            () =>
                {
                    var fileDialog = new System.Windows.Forms.OpenFileDialog();
                    var result = fileDialog.ShowDialog();
                    if (result != System.Windows.Forms.DialogResult.OK) return;
                    var file = fileDialog.FileName;
                    FileInput = file;
                    FileName = file.Split('\\').Last();
                });

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class CommandHandler : ICommand
        {
            private Action _action;
            private bool _canExecute;

            public CommandHandler(Action action, bool canExecute = true)
            {
                _action = action;
                _canExecute = canExecute;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return _canExecute;
            }

            public void Execute(object parameter)
            {
                _action();
            }
        }
    }
}
