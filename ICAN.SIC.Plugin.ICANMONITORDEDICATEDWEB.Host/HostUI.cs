using ICAN.SIC.Abstractions;
using ICAN.SIC.Abstractions.IMessageVariants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HostICAN.SIC.Plugin.ICANMONITORDEDICATEDWEB
{
    public partial class HostUI : Form
    {
        private ICAN.SIC.Plugin.ICANMONITORDEDICATEDWEB.ICANMONITORDEDICATEDWEB plugin = null;
        private IHub hub;

        public HostUI(IHub hub)
        {
            this.hub = hub;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            plugin?.Dispose();

            plugin = new ICAN.SIC.Plugin.ICANMONITORDEDICATEDWEB.ICANMONITORDEDICATEDWEB();
            plugin.Hub.PassThrough(hub);

            string targetChatApiLink = System.Configuration.ConfigurationSettings.AppSettings["TargetChatApiLink"];
            string targetMachineMessageApiLink = System.Configuration.ConfigurationSettings.AppSettings["TargetMachineMessageApiLink"];
            label1.Text = targetChatApiLink + "\n" + targetMachineMessageApiLink;
        }

        private delegate void SafeCallDelegate(string text);
        private void SetMessageText(string text)
        {
            if (textBox1.InvokeRequired)
            {
                var d = new SafeCallDelegate(SetMessageText);
                textBox1.Invoke(d, new object[] { text });
            }
            else
            {
                textBox1.Text = text;
            }
        }

        public void PrintMachineMessageResponse(IMachineMessage message)
        {
            Console.WriteLine("[Machine Message] " + message.Message);
            SetMessageText(message.Message);
        }

        public void PrintUserResponse(IUserResponse message)
        {
            Console.WriteLine("[User Message] " + message.Text);
            SetMessageText(message.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            plugin?.Dispose();
        }
    }
}
