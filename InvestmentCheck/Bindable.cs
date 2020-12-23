using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentCheck
{
    public class Bindable : INotifyPropertyChanged // OOP violation
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            //?. evaluating left side if it is null than continue to the right side
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void SetProperty<T>(ref T field, T value,
           [CallerMemberName] string name = null)
        {
            field = value;
            OnPropertyChanged(name);
        }
    }
}
