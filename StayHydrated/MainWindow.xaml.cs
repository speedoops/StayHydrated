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
using Gma.System.MouseKeyHook;
using Gma.System.MouseKeyHook.Implementation;

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

            SetToRunOnStartup();

            JobManager.Initialize();

            if (Properties.Settings.Default.DisplayOn)
            {
                StartJob();
            }
        }

        #region MouseKeyHook
        private IKeyboardMouseEvents m_GlobalHook;

        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
        }

        private void GlobalHookKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Console.WriteLine("KeyPress: \t{0}", e.KeyChar);
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            m_GlobalHook.Dispose();
        }
        #endregion

        public void ShowBalloon()
        {
            Balloon balloon = new Balloon();
            MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, Properties.Settings.Default.Duration);
        }

        public void ResetJob()
        {
            JobManager.RemoveAllJobs();
            StartJob();
        }

        public void StopJob()
        {
            JobManager.RemoveAllJobs();
            JobManager.Stop();
        }

        public void StartJob()
        {
            JobManager.AddJob(() => Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ShowBalloon();
            }), s => s.ToRunEvery(Properties.Settings.Default.Frequency).Minutes());
        }

        private void SetToRunOnStartup()
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

        private void RestBreakMenu_Click(object sender, RoutedEventArgs e)
        {
            RestBreakWindow.ShowWindow();
        }

        private void SettingsMenu_Click(object sender, RoutedEventArgs e)
        {
            Settings.ShowWindow();
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
