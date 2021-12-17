using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace PM2E3P201820110061.ViewModels
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            OnPropertyChange(propertyName);
            return true;

        }
    }
}
