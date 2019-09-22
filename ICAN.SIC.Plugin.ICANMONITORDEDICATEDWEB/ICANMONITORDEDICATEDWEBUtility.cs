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
                try
                {
                    result = client.DownloadString(url);
                }
                catch
                {

                }
            }

            return result;
        }
    }
}