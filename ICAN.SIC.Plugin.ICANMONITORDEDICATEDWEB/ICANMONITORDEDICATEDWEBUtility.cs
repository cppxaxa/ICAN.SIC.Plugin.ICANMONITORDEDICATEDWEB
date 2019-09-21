using System.Net;

namespace ICAN.SIC.Plugin.ICANMONITORDEDICATEDWEB
{
    public class ICANMONITORDEDICATEDWEBUtility
    {
        public string ReadWebLink(string url)
        {
            string result = "";

            using (WebClient client = new WebClient())
            {
                result = client.DownloadString(url);
            }

            return result;
        }
    }
}