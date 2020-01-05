using System;
using System.Windows.Input;

namespace MiniMongler {
    public class LambdaCommand : ITriggerCommand {
        public event EventHandler CanExecuteChanged;

        Func<LambdaCommand, object, bool> canExecute;
        Action<LambdaCommand, object> execute;

        public LambdaCommand(Func<LambdaCommand, object, bool> canExecute, Action<LambdaCommand, object> execute) {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public LambdaCommand(Action<LambdaCommand, object> execute) {
            this.canExecute = (l,p) => true;
            this.execute = execute;
        }

        public void Notify() => CanExecuteChanged?.Invoke(this, new EventArgs());
        public bool CanExecute(object parameter) => canExecute(this, parameter);
        public void Execute(object parameter) => execute(this, parameter);
    }
}
