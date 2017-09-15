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
        public Settings()
        {
            InitializeComponent();

            tbDuration.Text = "testing";
            tbFrequency.Text = "testing2";

            //checkForSavedSettings();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            //clean up notifyicon (would otherwise stay open until application finishes)
            //CustomCommandNotifyIcon.Dispose();
            //RoutedCommandNotifyIcon.Dispose();

            base.OnClosing(e);
        }
            
        private void applySavedSettings()
        {
            System.Console.WriteLine("Apply settings");
            Properties.Settings.Default.Duration = Int32.Parse(tbDuration.Text);
            Properties.Settings.Default.Frequency = Int32.Parse(tbFrequency.Text);
            Properties.Settings.Default.Save();
        }
    }
}
