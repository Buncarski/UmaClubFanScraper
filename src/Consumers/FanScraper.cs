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
        public JObject ScrapeFansJson(int circle_id, int year = -1, int month = -1)
        {
            string formattedUrl = string.Format(
                Data.UmaMoeUrl, 
                circle_id, 
                year == -1 ? DateTime.Today.Year : year, 
                month == -1 ? DateTime.Today.Month : month);
            JObject jsonObject = new JObject();
            try
            {
                var jsonString = httpClient.GetStringAsync(formattedUrl).Result;
                jsonObject = JObject.Parse(jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return jsonObject;
        }
    }
}
