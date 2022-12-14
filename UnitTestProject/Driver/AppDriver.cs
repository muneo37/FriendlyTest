using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using UnitTestProject.Driver.Window;

namespace UnitTestProject.Driver
{
    public class AppDriver : IDisposable
    {
        public Process Process { get; private set; }
        WindowsAppFriend _app;

        public MainWindow_Driver Main => new MainWindow_Driver(_app.Type<Application>().Current.MainWindow);

        public AppDriver()
        {
            if (Process != null)
            {
                return;
            }
            var dir = Path.GetFullPath("../../../../FriendlyTest/bin/x64/Debug/net6.0-windows");
            var pathExe = dir + "/FriendlyTest.exe";
            var info = new ProcessStartInfo(pathExe) { WorkingDirectory = dir };
            Process = Process.Start(pathExe);
            _app = new WindowsAppFriend(Process);
            WPFStandardControls_4.Injection(_app);
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
        }

        public void Dispose()
        {
            _app.Dispose();
            try
            {
                Process.Kill();
            }
            catch { }
            Process = null;
        }
    }
}
