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
        public MainWindow()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;            

            var job = new MyJob { window = this, icon = MyNotifyIcon };
            var registry = new Registry();
            JobManager.Initialize(registry);
            
            JobManager.AddJob(job, (s) => s.ToRunEvery(5).Seconds());
        }  
        
        public class MyJob : IJob
        {
            public MainWindow window { get; set; }            
            public TaskbarIcon icon { get; set; }

            public void Execute()
            {
                Balloon balloon = new Balloon();

                System.Console.WriteLine("wtf");
                String path = @"Reminders.txt";
                var lines = File.ReadAllLines(path);
                int count = lines.Count();
                Random rnd = new Random();
                int skip = rnd.Next(0, count);
                string line = lines.Skip(skip).First();

                balloon.BalloonText = line;

                //show balloon and close it after 2 seconds
                window.MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Fade, 4000);                
            }
        }
    }
}
