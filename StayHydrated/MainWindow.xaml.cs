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

            JobManager.Initialize(new MyRegistry());

            JobManager.AddJob(() => Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ShowBalloon();
            }), s => s.ToRunNow().AndEvery(Properties.Settings.Default.Frequency).Seconds());
        }

        public void ShowBalloon()
        {
            Balloon balloon = new Balloon();
            MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, Properties.Settings.Default.Duration);
        }

        public void resetJob()
        {
            JobManager.RemoveAllJobs();
            JobManager.AddJob(() => Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ShowBalloon();
            }), s => s.ToRunNow().AndEvery(Properties.Settings.Default.Frequency).Seconds());
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings(this);
            settings.Show();
        }
    }

    public class MyRegistry : Registry
    {
        public MyRegistry() { }
    }
}
