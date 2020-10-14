using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookServiceXamarinClientApp.Frameworks
{
    public class BindableBase:INotifyPropertyChanged
    {
        public BindableBase()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyHandler(string propertyName)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
        public bool Set<T>(ref T item,T value,[CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item,value))
            {
                item = value;
                PropertyHandler(propertyName);
                return true;
            }
            return false;
        }
    }
}
