using System;
using System.Windows;
using System.Windows.Threading;

namespace SharePointULSCatcher
{
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        static void App_DispatcherUnhandledException(Object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            e.Handled = true;
        }
    }
}