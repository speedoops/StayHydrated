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
        private int duration;
        private int frequency;
        private Boolean display;

        public MainWindow()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;

            SettingsUtil settings = new SettingsUtil();
            duration = settings.getDuration();
            frequency = settings.getFrequency();
            display = settings.getDisplay();
            JobManager.Initialize(new MyRegistry());

            JobManager.AddJob(() => Application.Current.Dispatcher.Invoke((Action)delegate
            {

                ShowBalloon("ayy lmao", DateTime.Now.ToString());

            }), s => s.ToRunNow().AndEvery(5).Seconds());
        }

        public void ShowBalloon(string title, string text)
        {
            Balloon balloon = new Balloon();
            MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, 4000);
        }
    }

    public class MyRegistry : Registry
    {
        public MyRegistry() { }
    }
}
