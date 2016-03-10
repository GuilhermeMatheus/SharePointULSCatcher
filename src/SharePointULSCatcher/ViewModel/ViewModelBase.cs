using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SharePointULSCatcher.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] String property = null)
        {
            var handler = PropertyChanged;
            if (handler == null)
                return;

            var args = new PropertyChangedEventArgs(property);
            handler(this, args);
        }
    }
}
