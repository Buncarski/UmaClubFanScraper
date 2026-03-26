using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace UmaClubFanScraper
{
    public class Scraper
    {
        private HttpClient httpClient = new HttpClient();
        //public Dictionary<string, long> trainers = new Dictionary<string, long>();
        public void Run()
        {
            Console.WriteLine("Beginning Scraping of fans for the desired uma club.");
            ScrapeFans();
        }

        private void ScrapeFans()
        {

            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(Data.UmaMoeUrl);

                    Console.WriteLine(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
