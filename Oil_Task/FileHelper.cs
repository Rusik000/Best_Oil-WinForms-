using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil_Task
{
    public static class JsonFileHelper
    {
               

        public static void JSONSerialization(Department department)
        {

            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter($"DataBase/{Guid.NewGuid()}.json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;
                    serializer.Serialize(jw, department);
                }
            }
        }

        public static void JSONDeSerialization(ref Department department, string fileName)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader($"DataBase/{fileName}.json"))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    department = serializer.Deserialize<Department>(jr);
                }
            }
        }

        
    }
}
