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
        private static Hardcodet.Wpf.TaskbarNotification.TaskbarIcon myNotifyIcon;
        private static String line;
        private static Balloon balloon;

        public MainWindow()
        {
            InitializeComponent();

            myNotifyIcon = MyNotifyIcon;

            String path = @"Reminders.txt";
            var lines = File.ReadAllLines(path);
            int count = lines.Count();
            Random rnd = new Random();
            int skip = rnd.Next(0, count);
            string line = lines.Skip(skip).First();

            balloon = new Balloon();
            balloon.BalloonText = line;

            var registry = new Registry();
            registry.Schedule<MyJob>().ToRunNow().AndEvery(5).Seconds();
            JobManager.Initialize(registry);
        }

        public void showBalloon() { }

        public class MyJob : IJob
        {
            public void Execute()
            { 
                //show balloon and close it after 4 seconds
                myNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Fade, 2000);
            }
        }
    }
}
