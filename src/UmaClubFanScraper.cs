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

        public int dayToScrape = -1;

        public void Run()
        {
            GetDay();

            Console.WriteLine("Press Enter to begin scrapage.");
            Console.ReadLine();

            Console.WriteLine("Beginning Scraping of fans for the desired uma clubs.");

            var clubsToScrapeFor = JObject.Parse(File.ReadAllText("Sources\\CircleIds.json"))["Clubs"];
            Console.WriteLine($"Found {clubsToScrapeFor.Value<JArray>().Count} clubs to scrape for.");


            foreach(JToken club in clubsToScrapeFor)
            {
                Console.WriteLine($"Beginning scraping for club: {club["name"]}");
                var jsonObject = fanScraper.ScrapeFansJson((int)club["circle_id"]);

                fileProcessor.SetTrainerNamesAsKeys(fileProcessor.ReadTrainers((string)club["name"]), out trainers);
                fileProcessor.AssignFansToTrainersFromJson(jsonObject, trainers, dayToScrape);

                fileProcessor.CreateFile((string)club["name"], trainers);

                Console.WriteLine($"Successfully processed club.");
            }

            Console.WriteLine("All clubs have been processed, please press enter to exit the tool thanks :).");
            Console.ReadLine();
        }

        public void GetDay()
        {
            Console.WriteLine("Type the day you wish to scrape for. (For the current day just press enter)");

            var day = Console.ReadLine();
            int dayNumber;
            if(int.TryParse(day, out dayNumber))
            {
                if(dayNumber > DateTime.Today.Day || dayNumber < 0)
                {
                    Console.WriteLine("Invalid day has been stated. Will use current day.");
                }
                else
                {
                    dayToScrape = dayNumber;
                }
            }
        }
    }
}
