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
            System.Console.WriteLine("Hello, World!");
            var registry = new Registry();
            registry.Schedule<MyJob>().ToRunNow().AndEvery(2).Seconds();
            JobManager.Initialize(registry);

            var notification = new Window1();
            notification.Show();
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
