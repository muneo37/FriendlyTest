using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows.Grasp;
using RM.Friendly.WPFStandardControls;
using System.Windows.Forms;

namespace TestProject.Driver.Window
{
    internal class MainWindow_Driver
    {
        AppVar _core;

        WPFButtonBase Button_登録 => new WPFButtonBase(_core.LogicalTree().ByType<Button>().Single());


        public MainWindow_Driver(AppVar core)
        {
            _core = core;
        }

        //public MessageBoxDriver Button_Click()
        //{
        //    var async = new Async();
        //    Button_登録.EmulateClick(async);
        //    var msg = WindowControl.WaitForIdentifyFromWindowText(Button_登録.App, "Error", async);
        //    return msg == null ? null : new MessageBoxDriver(msg);
        //}

    }
}
