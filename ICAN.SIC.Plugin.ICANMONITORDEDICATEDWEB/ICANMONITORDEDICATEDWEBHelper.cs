using ICAN.SIC.Abstractions;
using ICAN.SIC.Abstractions.ConcreteClasses;
using ICAN.SIC.Abstractions.IMessageVariants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICAN.SIC.Plugin.ICANMONITORDEDICATEDWEB
{
    public class ICANMONITORDEDICATEDWEBHelper
    {
        private string targetChatApiLink;
        private string targetMachineMessageApiLink;
        private int sleepInterval;

        private ICANMONITORDEDICATEDWEBUtility utility = new ICANMONITORDEDICATEDWEBUtility();

        private IHub hub;


        private Thread listenerThread = null;

        public ICANMONITORDEDICATEDWEBHelper(IHub hub, string targetChatApiLink, string targetMachineMessageApiLink, int sleepInterval)
        {
            this.hub = hub;
            this.targetChatApiLink = targetChatApiLink;
            this.targetMachineMessageApiLink = targetMachineMessageApiLink;
            this.sleepInterval = sleepInterval;
        }

        private void Worker()
        {
            while(true)
            {
                string chatJson = utility.ReadWebLink(targetChatApiLink);
                string machineMessageJson = utility.ReadWebLink(targetMachineMessageApiLink);

                var chatArray = JsonConvert.DeserializeObject<List<string>>(chatJson);
                var machineMessageArray = JsonConvert.DeserializeObject<List<string>>(machineMessageJson);

                if (chatArray != null && machineMessageArray != null)
                {
                    double interval = 0;
                    foreach (var item in chatArray)
                    {
                        hub.Publish<IUserResponse>(new UserResponse(item), TimeSpan.FromSeconds(interval));
                        interval += 0.5;
                    }
                    foreach (var item in machineMessageArray)
                    {
                        hub.Publish<IMachineMessage>(new MachineMessage(item), TimeSpan.FromSeconds(interval));
                        interval += 0.5;
                    }
                }

                Thread.Sleep(this.sleepInterval);
            }
        }

        public void StartListener()
        {
            this.listenerThread?.Abort();
            this.listenerThread = null;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[ICANMONITORDEDICATEDWEB] Start Listener");
            Console.ResetColor();

            this.listenerThread = new Thread(new ThreadStart(Worker));
            this.listenerThread.Start();
        }

        public void StopListener()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ICANMONITORDEDICATEDWEB] Stop Listener");
            Console.ResetColor();

            this.listenerThread.Abort();
            this.listenerThread = null;
        }

        public void Dispose()
        {
            this.listenerThread.Abort();
            this.listenerThread = null;
        }
    }
}
