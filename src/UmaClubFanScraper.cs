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
            Console.WriteLine("Beginning Scraping of fans for the desired uma club.");
            var jsonObject = fanScraper.ScrapeFansJson();
            fileProcessor.SetTrainerNamesAsKeys(fileProcessor.ReadTrainers(), out trainers);
            fileProcessor.AssignFansToTrainersFromJson(jsonObject, trainers);
            Console.WriteLine(trainers);
        }
    }
}
