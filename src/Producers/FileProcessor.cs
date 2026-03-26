using System;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Processors
{
    public class FileProcessor
    {
        public IList<string> ReadTrainers()
        {
            IList<string> trainerList = new List<string>();

            try
            {
                //src\Sources\Clubs\SBC 2.txt
                string fileName = "D:\\Code\\UmaClubFanScraper\\UmaClubFanScraper\\src\\Sources\\Clubs\\SBC 2.txt";                
                trainerList = File.ReadAllLines(fileName).ToList();
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine($@"Successfully read the following number of trainers for club [] : {trainerList.Count}.");
            return trainerList;
        }

        public void SetTrainerNamesAsKeys(IList<string> trainerNames, out Dictionary<string, int> trainers)
        {
            trainers = new Dictionary<string, int>();

            foreach(string trainer in trainerNames)
            {
                trainers.Add(trainer, 0);
            }
        }

        public void AssignFansToTrainersFromJson(JObject fansJson, Dictionary<string, int> trainers)
        {
            try
            {
                if(fansJson["members"] == null)
                {
                    throw new Exception("Members list not found.");
                }

                JArray membersList = (JArray)fansJson["members"];

                foreach(JToken member in membersList)
                {
                    string trainerName = (string)member["trainer_name"];

                    if (trainers.ContainsKey(trainerName))
                    {
                        int fans = GetFansForTrainer(20, member);
                        trainers[trainerName] = fans;
                    }
                }
                
                Console.WriteLine(trainers);
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public int GetFansForTrainer(int day, JToken member)
        {
            JArray fansArray = (JArray)member["daily_fans"];
            var value = fansArray[day].Value<int>();

            return value;
        }
    }
}
