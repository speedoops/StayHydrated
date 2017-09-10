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

namespace StayHydrated
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var registry = new Registry();
            registry.Schedule<MyJob>().ToRunNow().AndEvery(2).Seconds();

            Balloon balloon = new Balloon();
            balloon.BalloonText = "Stay Hydrated";

            //show balloon and close it after 4 seconds
            MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Fade, 10000);

            JobManager.Initialize(registry);
        }
    }

    public class MyJob : IJob
    {
        public void Execute()
        {
            System.Console.WriteLine("Don't forget to drink some water.");
        }
    }
}
