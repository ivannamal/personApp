using System;
using System.Windows.Input;

namespace BirthdayApp
{
    public partial class RelayCommand : ICommand
    {
        private readonly Action execute;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action executeAction)
        {
            execute = executeAction;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            execute();
        }
    }
}
