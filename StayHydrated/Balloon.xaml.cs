using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hardcodet.Wpf.TaskbarNotification;
using System.IO;

namespace StayHydrated
{
    public partial class Balloon : UserControl
    {
        private bool isClosing = false;

        #region BalloonText dependency property

        public static readonly DependencyProperty BalloonTextProperty =
            DependencyProperty.Register("BalloonText",
                typeof(string),
                typeof(Balloon),
                new FrameworkPropertyMetadata(""));

        public string BalloonText
        {
            get { return (string)GetValue(BalloonTextProperty); }
            set { SetValue(BalloonTextProperty, value); }
        }

        #endregion

        public Balloon()
        {
            InitializeComponent();
            text.Text = getRandomLine();
            TaskbarIcon.AddBalloonClosingHandler(this, OnBalloonClosing);
        }


        private String getRandomLine()
        {
            String path = @"Reminders.txt";
            var lines = File.ReadAllLines(path);
            int count = lines.Count();
            Random rnd = new Random();
            int skip = rnd.Next(0, count);
            return lines.Skip(skip).First();
        }

        private void OnBalloonClosing(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            isClosing = true;
        }

        private void ImgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
            taskbarIcon.CloseBalloon();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isClosing) return;

            TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
            taskbarIcon.ResetBalloonCloseTimer();
        }

        private void OnFadeOutCompleted(object sender, EventArgs e)
        {
            Popup pp = (Popup)Parent;
            pp.IsOpen = false;
        }
    }
}