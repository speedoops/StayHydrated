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

            //SettingsUtil settings = new SettingsUtil();
            //settings.
            JobManager.Initialize(new MyRegistry());

            JobManager.AddJob(() => ShowBalloon("ayy lmao", DateTime.Now.ToString()), s => s.ToRunNow().AndEvery(5).Seconds());
        }

        public void ShowBalloon(string title, string text) => MyNotifyIcon.ShowBalloonTip(title, text, MyNotifyIcon.Icon);
    }

    public class MyRegistry : Registry
    {
        public MyRegistry() { }
    }
}
