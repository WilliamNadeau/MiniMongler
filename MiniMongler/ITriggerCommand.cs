using System.Windows.Input;

namespace MiniMongler {
    public interface ITriggerCommand : ICommand {
        void Notify();
    }
}