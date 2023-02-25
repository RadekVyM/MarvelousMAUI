using System.Windows.Input;

namespace Marvelous.Core
{
    public class RelayCommand : ICommand
    {
        Action execute;
        Action<object> parameterExecute;
        Func<bool> canExecute;
        Func<object, bool> parameterCanExecute;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            parameterExecute = execute;
            parameterCanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            bool b = true;

            if (canExecute is null && parameterCanExecute is null)
                b = true;
            else if (canExecute is not null)
                b = canExecute();
            else if (parameterCanExecute is not null)
                b = parameterCanExecute(parameter);

            return b;
        }

        public void Execute(object parameter)
        {
            if (execute is not null)
                execute();
            if (parameterExecute is not null)
                parameterExecute(parameter);
        }
    }
}
