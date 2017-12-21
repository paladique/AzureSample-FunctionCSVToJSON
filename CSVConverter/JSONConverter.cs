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
       public static void Run([BlobTrigger("to-convert/{name}", Connection = "CSVStorage")]Stream myBlob, string name, TraceWriter log)
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
            var sReader = new StreamReader(blob);
            var csv = new CsvReader(sReader);

            csv.Read();
            csv.ReadHeader();

            var csvRecords = csv.GetRecords<object>().ToList();
           
            return JsonConvert.SerializeObject(csvRecords);
        }
    }
}