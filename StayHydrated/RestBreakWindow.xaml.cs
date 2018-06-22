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
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F4)
                this.Close();

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.F5)
                this.Close();

            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
                this.Close();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Shell32.ShellClass objShel = new Shell32.ShellClass();
            objShel.ToggleDesktop();
            this.Close();
        }
    }
}