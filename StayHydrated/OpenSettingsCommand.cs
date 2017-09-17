using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StayHydrated
{
    public class OpenSettingsCommand : ICommand
    {
        public void Execute(object parameter)
        {
            Settings settings = new Settings(parameter as MainWindow);
            settings.Show();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
