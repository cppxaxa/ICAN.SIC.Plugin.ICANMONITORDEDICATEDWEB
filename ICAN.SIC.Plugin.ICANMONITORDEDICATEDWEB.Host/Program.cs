using ICAN.SIC.Abstractions.IMessageVariants;
using ICAN.SIC.PubSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HostICAN.SIC.Plugin.ICANMONITORDEDICATEDWEB
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Hub hub = new Hub("Host");
            HostUI ui = new HostUI(hub);

            hub.Subscribe<IUserResponse>(ui.PrintUserResponse);
            hub.Subscribe<IMachineMessage>(ui.PrintMachineMessageResponse);
            
            Application.Run(ui);
        }
    }
}
