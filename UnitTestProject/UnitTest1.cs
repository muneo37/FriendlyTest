using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WpfApplication;
using System.Linq;
using UnitTestProject.Driver;
using System.IO;
using System.Diagnostics;
using Codeer.Friendly;
using RM.Friendly.WPFStandardControls;
using System.Windows;
using Codeer.Friendly.Dynamic;
using System.Windows.Controls;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using UnitTestProject.Driver.Window;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        WindowsAppFriend _app;

        [TestInitialize]
        public void TestInitialize()
        {
            var dir = Path.GetFullPath("../../../../FriendlyTest/bin/x64/Debug/net6.0-windows");
            var pathExe = dir + "\\FriendlyTest.exe";
            //var dir = Path.GetFullPath("D:/source/repos/VisualStudio2022/WPFFriendlySampleDotNetConf2016-master/WPFFriendlySampleDotNetConf2016-master/Project/WpfApplication/bin/Release");
            //var pathExe = dir + "/WpfApplication.exe";
            var info = new ProcessStartInfo(pathExe) { WorkingDirectory = dir };
            var process = Process.Start(pathExe);
            //var targetApp = Process.GetProcessesByName("FriendlyTest")[0];

            var dllPath = "C:\\Program Files\\dotnet\\shared\\Microsoft.NETCore.App\\6.0.8\\coreclr.dll";
            _app = new WindowsAppFriend(process, dllPath);
            //_app = new WindowsAppFriend(process, "4.0");
            //WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(_app.ProcessId).Kill();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // .netのタイプからウィンドウを取得

            var window = _app.Type<Application>().Current.MainWindow;

            window.Title = "タイトル変更";

            var btn = new WPFButtonBase(window.button1);

            var async = new Async();
            btn.EmulateClick(async);
            var msg = WindowControl.WaitForIdentifyFromWindowText(btn.App, "Title", async);

            if(msg != null)
            {
                var mbd = new MessageBoxDriver(msg);
                mbd.Button_OK_Click();
            }
            

            // フィールドからユーザーコントロールを取得(Friendlyの基本機能)
            //AppVar userControl = window.Dynamic()._userControl;

            //// バインディングからDataGridを取得
            //WPFDataGrid dataGrid = userControl.LogicalTree().ByBinding("SelectedItem.Value").Single().Dynamic();

            //// edit.
            //dataGrid.EmulateChangeCellText(1, 2, "abc");

            //var entry = _app.Main;

            //entry.button_click();
        }
    }
}
