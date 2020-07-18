using System;
using System.Windows.Input;

namespace WMDesktopUI.Helpers
{
    class DelegateCommandHelper : ICommand
    {
        Predicate<object> canExecute;
        Action<object> execute;
        public DelegateCommandHelper(Predicate<object> _canexecute, Action<object> _execute)
       : this()
        {
            canExecute = _canexecute;
            execute = _execute;
        }
        public DelegateCommandHelper()
        { }
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
