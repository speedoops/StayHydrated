using System;
using System.Windows;
using System.Windows.Input;

namespace StayHydrated
{
    public class ShowSettingsCommand : ICommand
    {
        public void Execute(object parameter)
        {
            //MessageBox.Show(parameter.ToString());
            Settings.ShowWindow();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}