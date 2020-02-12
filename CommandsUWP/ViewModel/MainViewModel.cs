using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CommandsUWP.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private int _number;
        private int _max;
        private Random _random;

        public MainViewModel()
        {
            _random = new Random();
            Roll = new RelayCommand(
                () => { Number = _random.Next(Max); },
                () => { return (Max > 0 && Max <= 6); });

            SetMax = new ParametrizedRelayCommand(
                (parameter) =>
                {
                    if (Int32.TryParse(parameter as string, out int result))
                    { Max = result; }
                    if (Roll.CanExecute(null)) Roll.Execute(null);
                });
        }

        public int Number
        {
            get
            {
                return _number +1;
            }
            set
            {
                _number = value;
                NotifyPropertyChanged();
            }
        }
        public int Max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
                Roll.RaiseCanExcuteChanged();
                NotifyPropertyChanged();
            }
        }

        public RelayCommand Roll { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

    }
}
