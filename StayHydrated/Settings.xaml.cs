using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace StayHydrated
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        MainWindow window;
        private bool displayState;
        private bool toggleState;

        public Settings(MainWindow mainWindow)
        {
            InitializeComponent();
            window = mainWindow;
            setTextboxes();
        }

        private void setTextboxes()
        {
            tbDuration.Value = (Properties.Settings.Default.Duration / 1000);
            tbFrequency.Value = Properties.Settings.Default.Frequency;
            if (Properties.Settings.Default.DisplayOn)
            {
                displayState = true;
                toggleState = true;
                DisplayToggle.IsChecked = true;
            } else
            {
                displayState = false;
                toggleState = false;
                DisplayToggle.IsChecked = false;
            }
        }
            
        private void applySavedSettings()
        {
            System.Console.WriteLine("Apply settings");            
            Properties.Settings.Default.Duration = ((int) tbDuration.Value)*1000;
            Properties.Settings.Default.Frequency = (int) tbFrequency.Value;
            if (toggleState && !displayState)
            {
                System.Console.WriteLine("Turn on display");
                Properties.Settings.Default.DisplayOn = true;
                window.startJob();
            } else if (!toggleState && displayState)
            {
                System.Console.WriteLine("Turn off display");
                Properties.Settings.Default.DisplayOn = false;
                window.stopJob();
            }
            if(StartupToggle.IsChecked == true)
            {
                if (StartUpManager.IsUserAdministrator())
                {
                    StartUpManager.AddApplicationToAllUserStartup();
                }
                else
                {
                    StartUpManager.AddApplicationToCurrentUserStartup();
                }
            } else
            {
                if (StartUpManager.IsUserAdministrator())
                {
                    StartUpManager.RemoveApplicationFromAllUserStartup();
                }
                else
                {
                    StartUpManager.RemoveApplicationFromCurrentUserStartup();
                }
            }
            Properties.Settings.Default.Save();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            applySavedSettings();
            window.resetJob();
        }

        private void DisplayToggle_Checked(object sender, RoutedEventArgs e)
        {
            toggleState = true;
            DisplayToggle.IsChecked = true;
        }

        private void DisplayToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            toggleState = false;
            DisplayToggle.IsChecked = false;
        }

        private void StartupToggle_Checked(object sender, RoutedEventArgs e)
        {
            StartupToggle.IsChecked = true;
        }

        private void StartupToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            StartupToggle.IsChecked = false;
        }
    }
}
