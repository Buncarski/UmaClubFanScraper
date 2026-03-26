using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Xsl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Consumers
{
    public class FanScraper
    {
        private HttpClient httpClient = new HttpClient();
        public FanScraper(){}
        public JObject ScrapeFansJson()
        {
            JObject jsonObject = new JObject();
            try
            {
                var jsonString = httpClient.GetStringAsync(Data.UmaMoeUrl).Result;
                jsonObject = JObject.Parse(jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine("Encountered an issue:");
                Console.WriteLine(e.Message);
            }

            return jsonObject;
        }
    }
}
