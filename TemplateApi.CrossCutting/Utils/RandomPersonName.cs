using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TemplateApi.CrossCutting.Utils
{
    public class RandomPersonName
    {
        private readonly NameList nameList;
        private readonly Random random;

        public RandomPersonName()
        {
            string startupPath = Directory.GetCurrentDirectory();
            var file = Path.Combine(startupPath, "names.json");
            random = new Random();
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                nameList = JsonSerializer.Deserialize<NameList>(json) ?? new NameList();
            }
        }

        public string GetName() => nameList.names[random.Next(nameList.names.Count)];


        private class NameList
        {
            public List<string> names { get; set; } = new List<string>(1000);
        }
    }


}
