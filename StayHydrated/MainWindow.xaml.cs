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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FluentScheduler;
using System.Diagnostics;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows.Controls.Primitives;
using System.IO;

namespace StayHydrated
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow AppMainWindow { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;

            setToRunOnStartup();

            JobManager.Initialize(new MyRegistry());

            if (Properties.Settings.Default.DisplayOn)
            {
                startJob();
            }
        }

        public void ShowBalloon()
        {
            Balloon balloon = new Balloon();
            MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, Properties.Settings.Default.Duration);
        }

        public void resetJob()
        {
            JobManager.RemoveAllJobs();
            startJob();
        }

        public void stopJob()
        {
            JobManager.RemoveAllJobs();
            JobManager.Stop();
        }

        public void startJob()
        {
            JobManager.AddJob(() => Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ShowBalloon();
            }), s => s.ToRunEvery(Properties.Settings.Default.Frequency).Minutes());
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings(this);
            settings.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void setToRunOnStartup()
        {
            if (StartUpManager.IsUserAdministrator())
            {
                StartUpManager.AddApplicationToAllUserStartup();
            }
            else
            {
                StartUpManager.AddApplicationToCurrentUserStartup();
            }
        }

        public class MyRegistry : Registry
        {
            public MyRegistry() { }
        }
    }
}
