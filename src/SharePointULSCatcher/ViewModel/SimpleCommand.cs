using System;
using System.Windows.Input;

namespace SharePointULSCatcher.ViewModel
{
    public class SimpleCommand : ICommand
    {
        private readonly Action<Object> _Action;

        public bool CanExecute(object parameter) { return true; }
        public event EventHandler CanExecuteChanged;

        public SimpleCommand(Action action)
            : this(_ => action?.Invoke())
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
        }

        public SimpleCommand(Action<Object> action)
        {
            _Action = action;
        }

        public void Execute(object parameter)
        {
            _Action(parameter);
        }
    }
}