using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PointagePresencePdc.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        // SetField (Name, value); // where there is a data member
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string property = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(property);
            return true;
        }
        // SetField(()=> somewhere.Name = value; somewhere.Name, value) 
        // Advanced case where you rely on another property
        protected bool SetProperty<T>(T currentValue, T newValue, Action doSet, [CallerMemberName] string property = null)
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue)) return false;
            doSet.Invoke();
            OnPropertyChanged(property);
            return true;
        }
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
    public class BaseViewModel<T> : BaseViewModel where T : class
    {
        public T Model
        {
            get => _mModel;
            set => SetProperty(ref _mModel, value);
        }

        private T _mModel;
    }
}
