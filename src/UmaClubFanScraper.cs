using System;
using System.ComponentModel.Design;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Consumers;
using Newtonsoft.Json.Linq;
using Processors;

namespace main
{
    public class UmaClubFanScraper
    {
        public FanScraper fanScraper = new FanScraper();
        public FileProcessor fileProcessor = new FileProcessor();
        public Dictionary<string, int> trainers;
        public void Run()
        {

            Console.WriteLine("Beginning Scraping of fans for the desired uma clubs.");
            
            //string path =new DirectoryInfo(Environment.CurrentDirectory).FullName;

            //Console.WriteLine(path);


            var clubsToScrapeFor = JObject.Parse(File.ReadAllText("Sources\\CircleIds.json"))["Clubs"];
            Console.WriteLine($"Found {clubsToScrapeFor.Value<JArray>().Count} clubs to scrape for.");
            foreach(JToken club in clubsToScrapeFor)
            {
                Console.WriteLine($"Beginning scraping for club: {club["name"]}");
                var jsonObject = fanScraper.ScrapeFansJson((int)club["circle_id"]);

                fileProcessor.SetTrainerNamesAsKeys(fileProcessor.ReadTrainers((string)club["name"]), out trainers);
                fileProcessor.AssignFansToTrainersFromJson(jsonObject, trainers);

                fileProcessor.CreateFile((string)club["name"], trainers);

                Console.WriteLine($"Successfully processed club.");
            }
        }
    }
}
