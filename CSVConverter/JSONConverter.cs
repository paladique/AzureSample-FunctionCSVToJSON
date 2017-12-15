using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using CsvHelper;

namespace CSVConverter
{
    public static class JSONConverter
    {
        [FunctionName("JSONConverter")]
       public static void Run([BlobTrigger("to-convert/{name}", Connection = "")]Stream myBlob, string name, TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            //Only convert CSV files
            if (name.Contains(".csv"))
            {
                var json = Convert(myBlob);
                log.Info(json);
            }
            else
            {
                log.Info("Not a CSV");
            }

           
        }

        public static string Convert(Stream blob)
        {
            var t = new StreamReader(blob);
            var csv = new CsvReader(t);

            csv.Read();
            csv.ReadHeader();

            var file = csv.GetRecords<object>().ToList();
           
            return JsonConvert.SerializeObject(file);
        }
    }
}