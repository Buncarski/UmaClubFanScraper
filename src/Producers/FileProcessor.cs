using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Processors
{
    public class FileProcessor
    {
        public IList<string> ReadTrainers(string clubName)
        {
            IList<string> trainerList = new List<string>();
            string fileName = String.Format(Data.clubTrainerNamesFilePath, clubName);                
            try
            {
                trainerList = File.ReadAllLines(fileName).ToList();
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine($@"Successfully read the following number of trainers for club {Path.GetFileNameWithoutExtension(fileName)} : {trainerList.Count}.");
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

        public void AssignFansToTrainersFromJson(JObject fansJson, Dictionary<string, int> trainers, int day = -1)
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
                        int fans = GetFansForTrainer(member);
                        trainers[trainerName] = fans;
                    }
                }
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public int GetFansForTrainer(JToken member, int day = -1)
        {
            JArray fansArray = (JArray)member["daily_fans"];
            var value = day==-1 ? fansArray[DateTime.Today.Day-1].Value<int>() : fansArray[day-1].Value<int>();

            return value;
        }

        public void CreateFile(string clubName, Dictionary<string, int> trainerFanPairs, bool includeNames = false)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var item in trainerFanPairs)
            {
                if (includeNames)
                {
                    sb.Append($"{item.Key}={item.Value}\n");
                } else
                {
                    sb.Append($"{item.Value}\n");
                }
            }

            File.WriteAllText(String.Format(Data.outputPath, clubName), sb.ToString());
        }
    }
}
