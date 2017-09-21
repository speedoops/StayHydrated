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
    public partial class Settings : MetroWindow
    {
        private MainWindow window;
        private bool displayState;
        private bool toggleState;
        private const int ONE_SECOND = 1000;

        public Settings(MainWindow mainWindow)
        {
            InitializeComponent();
            window = mainWindow;
            SetTextboxes();
        }

        private void SetTextboxes()
        {
            tbDuration.Value = (Properties.Settings.Default.Duration / ONE_SECOND);
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
            StartupToggle.IsChecked = Properties.Settings.Default.StartupOn;
        }
            
        private void ApplySavedSettings()
        {
            Properties.Settings.Default.Duration = ((int) tbDuration.Value)* ONE_SECOND;
            Properties.Settings.Default.Frequency = (int) tbFrequency.Value;
            if (toggleState && !displayState)
            {
                Properties.Settings.Default.DisplayOn = true;
                window.StartJob();
            } else if (!toggleState && displayState)
            {
                Properties.Settings.Default.DisplayOn = false;
                window.StopJob();
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
            ApplySavedSettings();
            window.ResetJob();
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
            Properties.Settings.Default.StartupOn = true;
            StartupToggle.IsChecked = true;
        }

        private void StartupToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.StartupOn = false;
            StartupToggle.IsChecked = false;
        }
    }
}
