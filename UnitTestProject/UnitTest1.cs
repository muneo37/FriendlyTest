using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;
using Codeer.Friendly;
using RM.Friendly.WPFStandardControls;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using UnitTestProject.Driver.Window;
using System.Drawing;

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
            var info = new ProcessStartInfo(pathExe) { WorkingDirectory = dir };
            var process = Process.Start(pathExe);

            var dllPath = "C:\\Program Files\\dotnet\\shared\\Microsoft.NETCore.App\\6.0.8\\coreclr.dll";
            _app = new WindowsAppFriend(process, dllPath);

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

            var window = _app.Type<System.Windows.Application>().Current.MainWindow;

            window.Title = "タイトル変更";

            window.Background = _app.Type<System.Windows.Media.Brushes>().Black;

            //左上
            double top = window.Top;
            double left = window.Left;
            double width = window.Width;
            double height = window.Height;

            Point pos = new Point((int)top, (int)left);
            Size size = new Size((int)width, (int)height);
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(pos.X, pos.X, 0, 0, size, CopyPixelOperation.SourceCopy);
                System.Windows.Clipboard.SetDataObject(bmp, true);
                System.Windows.Media.Imaging.BitmapSource img = System.Windows.Clipboard.GetImage();
                // BitmapSourceを保存する
                using (Stream stream = new FileStream("test.png", FileMode.Create))
                {
                    System.Windows.Media.Imaging.PngBitmapEncoder encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
                    encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(img));
                    encoder.Save(stream);
                }
            }


            var btn = new WPFButtonBase(window.button1);

            var async = new Async();
            btn.EmulateClick(async);
            var msg = WindowControl.WaitForIdentifyFromWindowText(btn.App, "Title", async);

            Assert.AreNotEqual(null, msg);
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
