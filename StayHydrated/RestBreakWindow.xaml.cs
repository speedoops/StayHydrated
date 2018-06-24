using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace StayHydrated
{
    public partial class RestBreakWindow : Window
    {
        #region Singlton
        private static readonly RestBreakWindow instance = new RestBreakWindow();        

        public static void ShowWindow()
        {
            instance.Show();
            if (instance.WindowState == WindowState.Minimized)
                instance.WindowState = WindowState.Normal;
        } 
        #endregion

        private RestBreakWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            this.Hide();
        }
    }
}