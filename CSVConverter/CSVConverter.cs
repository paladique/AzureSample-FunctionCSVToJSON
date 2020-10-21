using CsvHelper;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace CSVConverter
{
    public static class CSVConverter
    {

        [FunctionName("CSVConverter")]
        public static void Run([BlobTrigger("to-convert/{name}", Connection = "CSVStorage")] Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            //Only convert CSV files
            if (name.Contains(".csv"))
            {
                var json = Convert(myBlob);
                log.LogInformation(json);
            }
            else
            {
                log.LogInformation("Not a CSV");
            }
        }

        public static string Convert(Stream blob)
        {
            var sReader = new StreamReader(blob);
            var csv = new CsvReader((IParser)sReader);

            csv.Read();
            csv.ReadHeader();

            var csvRecords = csv.GetRecords<object>().ToList();

            return JsonConvert.SerializeObject(csvRecords);
        }
    }
}