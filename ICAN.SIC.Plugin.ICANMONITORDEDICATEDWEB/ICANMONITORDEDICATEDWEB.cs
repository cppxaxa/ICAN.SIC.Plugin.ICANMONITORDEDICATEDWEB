using ICAN.SIC.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAN.SIC.Plugin.ICANMONITORDEDICATEDWEB
{
    public class ICANMONITORDEDICATEDWEB : AbstractPlugin
    {
        private ICANMONITORDEDICATEDWEBHelper helper;
        private string targetChatApiLink, targetMachineMessageApiLink;
        private int sleepInterval;

        public ICANMONITORDEDICATEDWEB() : base("ICANMONITORDEDICATEDWEBv1")
        {
            targetChatApiLink = System.Configuration.ConfigurationSettings.AppSettings["TargetChatApiLink"];
            targetMachineMessageApiLink = System.Configuration.ConfigurationSettings.AppSettings["TargetMachineMessageApiLink"];
            sleepInterval = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["SleepInterval"]);

            helper = new ICANMONITORDEDICATEDWEBHelper(hub, targetChatApiLink, targetMachineMessageApiLink, sleepInterval);
            helper.StartListener();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[INFO] ICANMONITORDEDICATEDWEB Started");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tMonitor Chat Link: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(targetChatApiLink);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tMonitor Machine Message Link: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(targetMachineMessageApiLink);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tSleep Interval: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(sleepInterval);

            Console.ResetColor();
        }

        public override void Dispose()
        {
            helper.Dispose();
        }
    }
}
