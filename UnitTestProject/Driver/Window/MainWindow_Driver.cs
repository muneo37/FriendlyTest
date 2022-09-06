using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows.Grasp;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;

namespace UnitTestProject.Driver.Window
{
    public class MainWindow_Driver
    {
        AppVar _core;

        WPFButtonBase Button_上げ潮じゃー => new WPFButtonBase(_core.LogicalTree().ByType<Button>().Single());


        public MainWindow_Driver(AppVar core)
        {
            _core = core;
        }

        public MessageBoxDriver button_click()
        {
            var async = new Async();
            Button_上げ潮じゃー.EmulateClick(async);
            var msg = WindowControl.WaitForIdentifyFromWindowText(Button_上げ潮じゃー.App, "Error", async);
            return msg == null ? null : new MessageBoxDriver(msg);
        }

    }
}
